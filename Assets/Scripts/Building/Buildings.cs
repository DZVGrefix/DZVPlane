using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
    Ez hozza létre az épületeket
*/
public class Buildings : MonoBehaviour
{
    [Header("Building settings")]
    [SerializeField] int maxNumberOfBuildings = 27;             // hány darab épület lesz a pályán
    [SerializeField] int maxBuildingHeight = 12;                // milyen magas legyen egy épület
    [SerializeField] int minBuildingHeight = 6;                 // Minumum épület magasság

    [Header("Pool settings")]
    [SerializeField] PItemBuilding[] poolItems;                 // poolba rakando elemek

    BuildingItem[,] _buildingsItem;                             // pálya model a memoriában
    ObjectPooler _objPooler;                                    // poolra hívatkozás
    BuildingLevel _buildingLevel;
    int _scoreLevel = 0;


    // Játék indítása - GameManager hívja meg
    public Vector3[] StartGame(BuildingLevel buildingLevel)
    {
        _scoreLevel = 0;
        AddScore();
        _objPooler = ObjectPooler.Instance();
        _buildingLevel = buildingLevel;
        maxNumberOfBuildings = _buildingLevel.numberOfBuilding;
        maxBuildingHeight = _buildingLevel.maxHeight;
        minBuildingHeight = _buildingLevel.minHeight;

        // pálya tömb létrehozás
        _buildingsItem = new BuildingItem[maxNumberOfBuildings, maxBuildingHeight];

        // elemek hozzáadása a pool-hoz
        if (_objPooler != null)
        {
            _objPooler.AddPoolItems(poolItems);
        }

        return CreateMaps();
    }

    // épület poziciója a memóriában
    public void SetBuildingsArray(Vector3 pos, BuildingItem bItem)
    {
        _buildingsItem[(int)pos.x, (int)pos.y] = bItem;
    }

    // pálya létrehozás
    Vector3[] CreateMaps()
    {
        // köépen van a nulla ezért kiszámolom honnan kezdődjenek az épületek
        int width = (maxNumberOfBuildings / 2) * -1;

        // a ledobási pozició ott van ahol az épületek lesznek
        Vector3[] dropPos = new Vector3[maxNumberOfBuildings];

        for (int x = 0; x < maxNumberOfBuildings; x++, width++)
        {
            // elkészítem a ledobási poziciókat
            dropPos[x] = new Vector3(width, maxBuildingHeight + 4f, 0f);

            // milyen magas legyen az épület.
            int height = Random.Range(minBuildingHeight, maxBuildingHeight);

            BuildingType poolType = GetRandomPoolType();
            PoolItem[] filterPool = FilterPoolWithType(poolType);

            for (int y = 0; y < maxBuildingHeight; y++)
            {
                Vector3 itemPos = new Vector3(width, y, 0);
                Vector2 itemIndex = new Vector2(x, y);
                _buildingsItem[x, y] = GetBuildingItem(y, height, poolType, filterPool, itemPos, itemIndex);
            }
        }
        return dropPos;
    }

    /*
        épületek létrehozás
            - GetRandomPoolType: random épület tipus
            - FilterPoolWithType: leszüröm az épületek listát csak 1 adott épületre
            - RandomFloor: Random emelet tipus
            - RandomGround: Random földszint tipus
            - GetBuilding: egy épület elem létrehozás
            - GetBuildingItem: vissza adja az épület elemet
    */
    BuildingType GetRandomPoolType()
    {
        var v = System.Enum.GetValues(typeof(BuildingType));
        return (BuildingType)v.GetValue(Random.Range(0, v.Length));
    }

    PoolItem[] FilterPoolWithType(BuildingType poolType)
    {
        List<PItemBuilding> pools = poolItems.Where(x => x.buildingType == poolType).ToList();
        return pools.ToArray();
    }

    int RandomFloor(BuildingType pType)
    {
        if (pType == BuildingType.GlassBuildingH || pType == BuildingType.GlassBuildingV)
        {
            return 0;
        }
        return Random.Range(0, 2);
    }

    int RandomGround(BuildingType pType)
    {
        if (pType == BuildingType.GlassBuildingH || pType == BuildingType.GlassBuildingV)
        {
            return Random.Range(1, 4);
        }
        return Random.Range(2, 5);
    }

    BuildingItem GetBuildingItem(int currHeight, int maxHeight, BuildingType poolType, PoolItem[] pItems, Vector3 pos, Vector2 index)
    {
        if (currHeight == 0)
        {
            // földszint elem kell.
            return GetBuilding(pItems[RandomGround(poolType)].prefab.name, pos, index);
        }
        else if (currHeight <= maxHeight)
        {
            // emeletet sorsolunk még nincs tető
            return GetBuilding(pItems[RandomFloor(poolType)].prefab.name, pos, index);
        }
        return null;
    }

    BuildingItem GetBuilding(string name, Vector3 pos, Vector2 index)
    {
        // épület elem létrehozása
        GameObject go = _objPooler.SpawnFromPool(name, pos, Quaternion.identity, transform);

        // épület item beállítások
        BuildingItem buildingItem = go.GetComponent<BuildingItem>();
        buildingItem.Setup(this, pos, index);

        // épület élet beállítás
        BuildingHealth buildingHealth = go.GetComponent<BuildingHealth>();
        buildingHealth.Setup(this);

        return buildingItem;
    }
    // vége ... 

    // Visszarakom a poolba és a memoriában is ki "null"-ázom
    public void BackPoolItem(GameObject buildingGO)
    {
        // visszarakás a pool-ba
        BuildingItem bItem = buildingGO.GetComponent<BuildingItem>();
        Vector3 pos = bItem.indexPos;
        _buildingsItem[(int)pos.x, (int)pos.y] = null;

        _objPooler.ObjectToPool(buildingGO);
    }

    // épület elem zuhanás
    public void FallingBuildingItem()
    {
        int nullCounter = 0;
        for (int x = 0; x < maxNumberOfBuildings; x++)
        {
            for (int y = 0; y < maxBuildingHeight; y++)
            {
                if (_buildingsItem[x, y] == null)
                {
                    nullCounter++;
                }
                else if (nullCounter > 0)
                {
                    _buildingsItem[x, y].buildPos.y -= nullCounter;
                    _buildingsItem[x, y].indexPos.y -= nullCounter;
                    _buildingsItem[x, y].StartFall();

                    _buildingsItem[x, y - nullCounter] = _buildingsItem[x, y];
                    _buildingsItem[x, y] = null;
                }
            }
            nullCounter = 0;
        }
    }



    /*
        PONT SZÁM SZÁMOLÁSA
    */
    public void AddScore(int value = 0)
    {
        _scoreLevel += value;
        GameGUI.Instance().SetScoreText(_scoreLevel);
    }
    public int GetScore()
    {
        return _scoreLevel;
    }
}
