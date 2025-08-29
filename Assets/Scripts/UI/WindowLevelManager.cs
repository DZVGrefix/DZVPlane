using UnityEngine;

public class WindowLevelManager : MonoBehaviour
{
    [SerializeField] StartLevelButton[] buttons;

    // Level gombok beállítása 
    public void Setup()
    {
        //PlayerDM.Instance().Setup();
        PlayerData playerData = PlayerDM.Instance().GetPlayerData();
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].Setup((i > playerData.level), i, (i + 1).ToString());
        }
    }
}
