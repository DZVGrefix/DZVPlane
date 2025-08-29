using TMPro;
using UnityEngine;
using UnityEngine.UI;



/*
    pályák indítására
*/
[RequireComponent(typeof(Image))]
public class StartLevelButton : ButtonBehaviour<LevelNavigationManager>
{
    //[SerializeField] int level = 0;                     // Level szint meghatározás
    [SerializeField] Sprite lockSprite;
    [SerializeField] Sprite buttonSprite;
    [SerializeField] TMP_Text titleText;

    int _level;

    // pálya megjelenítés ha lock akkor a lakattal ha nem akkor aktiv gomb ként.
    public void Setup(bool isLock, int level, string lvl = "")
    {
        _level = level;
        Image img = GetComponent<Image>();
        if (isLock)
        {
            img.sprite = lockSprite;
            titleText.text = "";
            MyButton.interactable = !isLock;
        }
        else
        {
            img.sprite = buttonSprite;
            titleText.text = lvl;
            MyButton.interactable = !isLock;
        }
    }

    // OnClick esemény
    protected override void OnClick()
    {
        Manager.StartLevelButton(_level);
    }
}
