using System;
using UnityEngine;


/*
    Bomba ledobás vezérlője ez felel a bombák ledobásáért, melyik az aktiv bomba, hány bombát dobhatok le.
*/
public class DropBomb : MonoBehaviour
{
    [Header("Drop bomb settings")]
    [SerializeField] Transform dropPoint;               // Ledobási pont
    /* player data adatból ami rám vonatkozik az a: 
        - PlusDropBomb
    */

    Vector3[] _dropPositions;                           // hol lehet ledobni a bombákat
    BombItem[] _bombItems;                              // bombák 
    LimitedBomb[] _limitedBombs;                        // bombák tömből át veszi azokat az adatokat ami a limitált bombákhoz kell.
    ObjectPooler _objectPooler;                         // pool hivatkozás
    int _indexBombItem;                                 // bombItems tömb aktiv eleme
    int _currentBombs;                                  // bomba ledobás számláló

    Action _myDelegate;                                 // ez egy függvény ami a Buildings.cs -ből kapok meg. (Buildings::FallingBuildingItem)
    GameGUI _gameGUI;


    // Kezdő beállítások 
    public void Setup(PlayerController playerController, PlayerData playerData, Vector3[] dropPos, BombItem[] items, Action myDelegate)
    {
        _dropPositions = dropPos;
        _bombItems = items;
        SetLimitedBombs();
        _myDelegate = myDelegate;
        _objectPooler = ObjectPooler.Instance();
        _indexBombItem = 0;
        _currentBombs = playerController.numberOfBombs + playerData.PlusDropBomb;

        _gameGUI = GameGUI.Instance();
        RefreshGUI();
    }
    void SetLimitedBombs() {
        _limitedBombs = new LimitedBomb[_bombItems.Length];
        for (int i = 0; i < _bombItems.Length; i++)
        {
            LimitedBomb limitedBomb = new()
            {
                isLimited = _bombItems[i].prefab.isLimited,
                limitedBomb = _bombItems[i].prefab.limitedBomb
            };
            _limitedBombs[i] = limitedBomb;
        }
    }

    // az Object visszarakása a poolba
    public void Reload(GameObject obj, bool isBuilding = true)
    {
        if (obj != null)
        {
            _objectPooler.ObjectToPool(obj);
            if (isBuilding)
            {
                _myDelegate.Invoke();       // Buildings értesítés hogy pottyogjanak le az épület elemek
            }

            // ha repesz elem pusztul akkor nem kell növel a visszarakott bombák számát
            if (!obj.TryGetComponent(out Shrapnel shrapnel))
            {
                DropBombState(1);
            }
        }
    }

    // bomba ledobás
    public void Drop()
    {
        // ha van aktív bomba akkor dobok csak le
        if (_currentBombs > 0 && isLimitedBombDrop())
        {
            // bomba létrehozás és beállítása
            GameObject go = _objectPooler.SpawnFromPool(_bombItems[_indexBombItem].poolName, GetDropPosition(), dropPoint.rotation, null);
            Bomb bomb = go.GetComponent<Bomb>();
            bomb.Setup(this);

            DropBombState(-1);
        }
    }

    void DropBombState(int value) {
        _currentBombs += value;
        GameGUI.Instance().SetDropBombText(_currentBombs);
    }

    // válashtható bombák közti lépkedés
    public void PreviousBomb()
    {
        _indexBombItem = _indexBombItem == 0 ? _bombItems.Length - 1 : _indexBombItem - 1;
        RefreshGUI();
    }
    // válashtható bombák közti lépkedés
    public void NextBomb()
    {
        _indexBombItem = (_indexBombItem + 1) / (_bombItems.Length - 1);
        RefreshGUI();
    }
    // válashtható bombák közti lépkedés
    void RefreshGUI()
    {
        Sprite sprite = _bombItems[_indexBombItem].sprite;
        string strCount = "";
        if (_limitedBombs[_indexBombItem].isLimited) {
            strCount = _limitedBombs[_indexBombItem].limitedBomb.ToString();
        }
        _gameGUI.SetBombPanel(sprite, strCount);
    }
    bool isLimitedBombDrop()
    {
        LimitedBomb bItem = _limitedBombs[_indexBombItem];
        if (bItem.isLimited)
        {
            if (bItem.limitedBomb > 0)
            {
                bItem.limitedBomb--;
                RefreshGUI();
                return true;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    // repcsi poziciója alapján meghatározom hol dobhatok le bombát
    Vector3 GetDropPosition()
    {
        foreach (Vector3 pos in _dropPositions)
        {
            if (pos.x - .5f < dropPoint.position.x && pos.x + .5f > dropPoint.position.x)
            {
                return new Vector3(pos.x, dropPoint.position.y, dropPoint.position.z);
            }
        }
        return dropPoint.position;
    }

    void OnDrawGizmos()
    {
        if (_dropPositions == null && _dropPositions.Length == 0) return;

        Gizmos.color = Color.red;
        foreach (Vector3 pos in _dropPositions)
        {
            Gizmos.DrawWireSphere(pos, .5f);
            Gizmos.DrawLine(pos, new Vector3(pos.x, 0, 0));
        }
    }
}

//[System.Serializable]
public class LimitedBomb {
    public bool isLimited;
    public int limitedBomb;
}
