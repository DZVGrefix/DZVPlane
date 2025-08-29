using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameNavigationManager : MonoBehaviour
{
    const string LevelSceneName = "LevelScene";


    public void StartGameButtonClick()
    {
        //
        SceneManager.LoadScene(LevelSceneName);
    }

    public void QuitGameButtonClick()
    {
        Application.Quit();
    }
}
