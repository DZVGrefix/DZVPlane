using System.Collections.Generic;
using TMPro;
using UnityEngine;


/*
    Játék manager
*/
public class GameManager : MonoBehaviour
{
    [SerializeField] Buildings buildings;               // hivatkozás a buildingsre ami létrehozzáa pályát
    //[SerializeField] ShopManager shopManager;           // Shop menu manager


    PlayerDM _playerDM;                                 // player data manager hivatkozás
    PlayerData _playerData;                             // player Data
    PlayerController _playerController;                 // Player controller hivatkozás
    ObjectPooler _objPooler;                            // Pool-ra hivatkozás
    GameGUI _gameGUI;                                   // Game gui-ra hivatkozás
    BuildingDM _buildingDM;                             // BuildingDM-re hivatkozás
    int curLevel;                                       // pontszámok
    


    //Kezdő adatok bekérése
    void Start()
    {
        _playerDM = PlayerDM.Instance();
        _playerDM.Setup();
        _playerData = _playerDM.GetPlayerData();
        _objPooler = ObjectPooler.Instance();
        _gameGUI = GameGUI.Instance();
        _buildingDM = BuildingDM.Instance();
        _buildingDM.Setup();
        StartGame();
    }

    // játék indítás
    void StartGame()
    {
        // Épület indítás
        curLevel = GameStaticData.level;
        BuildingLevel buildingLevel = _buildingDM.GetBuildingLevelWithIndex(curLevel);

        Vector3[] dropPos = buildings.StartGame(buildingLevel);

        // Bombák lekérdezése
        ShopBombDM.Instance().Setup();
        BombItem[] bombItems = ShopBombDM.Instance().GetAllBuyBombItem();

        // Repülök
        ShopPlaneDM.Instance().Setup();
        ShopPlane shopPlane = ShopPlaneDM.Instance().GetShopPlaneWithIndex(_playerData.planeIndex);
        _playerController = Instantiate(shopPlane.shopPlaneItem.prefab, buildingLevel.startPlane, Quaternion.identity);
        _playerController.Setup(this, _playerData, buildingLevel, dropPos, bombItems, buildings.FallingBuildingItem);
    }

    

    /* RESTART GAME KEZDÉS */
    // törlöm a megmaradt épületeket.
    void DeleteBuildingItems()
    {
        int count = buildings.transform.childCount;
        for (int i = count - 1; i >= 0; i--)
        {
            buildings.BackPoolItem(buildings.transform.GetChild(i).gameObject);
        }
    }
    // újra indítom a játékot.
    public void RestartGame()
    {
        _objPooler.ClearActiveParent();
        DeleteBuildingItems();
        StartGame();
    }
    /* RESTART GAME VÉGE */



    






    /* JÁTÉK VÉGE VAGY NYERÉS KEZELÉS */
    public void GameOver()
    {
        Destroy(_playerController.gameObject);
        GameNavigation.Instance().ShowGameOverWindow();
    }
    public void GameWinner()
    {
        // pontszámokat kell megszerezni.
        int score = buildings.GetScore();
        LevelScore levelScore = _playerDM.GetLevelScoreWithLevel(_playerData.level);
        if (levelScore != null)
        {
            if (levelScore.highScore < score)
            {
                levelScore.highScore = score;
            }
            int coins = score / 10;
            _playerData.coins += coins;
            if (curLevel == _playerData.level)
            {
                _playerData.level++;
            }
            _playerDM.SetData(_playerData, 0);

            GameNavigation.Instance().ShowWinnerWindow(levelScore.highScore, score, coins);
        }
    }
}
