using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameNavigation : MonoBehaviour
{
    #region Singelton
    private static GameNavigation _instance;
    public static GameNavigation Instance()
    {
        return _instance;
    }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion
    [SerializeField] GameManager gameManager;
    [SerializeField] Window GameOverWindow;
    [SerializeField] Window WinnerWindow;

    const string LevelSceneName = "LevelScene";


    Window _activeWindow;


    /* ABLAKOK MEGJELENITÉSE */
    public void ShowGameOverWindow()
    {
        //
        ShowWindow(GameOverWindow);
    }

    public void ShowWinnerWindow(int highScore, int score, int coins)
    {
        //
        ShowWindow(WinnerWindow);
        if (_activeWindow.TryGetComponent(out WindowWinnerManager winnerManager))
        {
            winnerManager.Setup(highScore, score, coins);
        }
    }

    /* GOMBOK NYOMKODÁSA */
    public void RestartGameOverButton()
    {
        // Ablak bezárás és a játék újra indítása
        CloseWindow();
        gameManager.RestartGame();
    }
    public void CancelGameOverButton()
    {
        // Ablak bezárása és vissza lépés a Level Scene-re
        CloseWindow();
        SceneManager.LoadScene(LevelSceneName);
    }
    public void ContinueWinnerButton()
    {
        // Ablak bezárása
        CloseWindow();
        // adat mentés hogy sikeres a pálya vagyis lvl szint növelés
        /*
        PlayerData playerData =  PlayerDM.Instance().GetPlayerData();

        if (score > playerData.levelScores[playerData.level].highScore) {
            playerData.levelScores[playerData.level].highScore = score;    
        }
        playerData.coins += coins;
        playerData.level++;
        PlayerDM.Instance().SetData(playerData, 0);
        */

        // Vissza lépés a Level scene-re
        SceneManager.LoadScene(LevelSceneName);
    }



    private void ShowWindow(Window window)
    {
        CloseWindow();
        _activeWindow = Instantiate(window, transform);
    }
    private void CloseWindow()
    {
        if (_activeWindow != null)
        {
            _activeWindow.CloseWindow();
        }
    }
}
