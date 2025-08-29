using UnityEngine;
using UnityEngine.SceneManagement;


/*
    navigálás az ablakok közt
*/
public class LevelNavigationManager : MonoBehaviour
{
    [SerializeField] private Window levelWindow;                // Hívatkozás a pályák ablakra
    [SerializeField] private Window upgradeWindow;              // Hívatkozás a Fejlesztések ablakra
    [SerializeField] private Window shopWindow;                 // Hivatkozás a shop ablakra
    const string GameSceneName = "GameScene";
    const string StartGameSceneName = "StartScene";

    Window _activeWindow;



    public void ShowLevelWindow()
    {
        ShowWindow(levelWindow);
        if (_activeWindow.TryGetComponent(out WindowLevelManager levelManager))
        {
            levelManager.Setup();
        }
    }

    public void ShowUpgradeWindow()
    {
        ShowWindow(upgradeWindow);
        if (_activeWindow.TryGetComponent(out WindowUpgradeManager upgradeManager))
        {
            upgradeManager.Setup();
        }
    }

    public void ShowShopWindow()
    {
        ShowWindow(shopWindow);
        if (_activeWindow.TryGetComponent(out WindowShopManager shopManager))
        {
            shopManager.Setup();
        }
    }

    public void GoBackToLevelWindow()
    {
        ShowLevelWindow();
    }

    public void GoBackToStartWindow() {
        SceneManager.LoadScene(StartGameSceneName);
    }

    public void StartLevelButton(int lvl)
    {
        GameStaticData.level = lvl;
        SceneManager.LoadScene(GameSceneName);
    }

    private void ShowWindow(Window window)
    {
        if (_activeWindow != null)
        {
            _activeWindow.CloseWindow();
        }
        _activeWindow = Instantiate(window, transform);
    }
}
