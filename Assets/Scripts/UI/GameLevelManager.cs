using UnityEngine;

/*
    ez a LEVELSCENE-en van ez indítja el a level választó elemeket.
*/
public class GameLevelManager : MonoBehaviour
{
    [SerializeField] LevelNavigationManager levelNavigationManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerDM.Instance().Setup();
        ShopPlaneDM.Instance().Setup();
        ShopBombDM.Instance().Setup();

        levelNavigationManager.ShowLevelWindow();
    }
}
