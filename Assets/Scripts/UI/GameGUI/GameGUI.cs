using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameGUI : MonoBehaviour
{
    #region Singelton
    private static GameGUI _instance;
    public static GameGUI Instance()
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

    [Header("Bomb Drop UI")]
    [SerializeField] Image bombImage;                               // ez mutatja melyik az aktuális bomba 
    [SerializeField] TMP_Text bombCountText;                        // egy szám hogy mennyi van még belőle
    [SerializeField] TMP_Text bombDropText;                         // aktiv ledobhato bombák száma

    [Header("Score settings")]
    [SerializeField] TMP_Text scoreText;
    float displayScore = 0;
    float currentScore = 0f;
    float scoreSpeed = 2f;


    // Ezt a függvényt a DropBomb hívja
    public void SetBombPanel(Sprite sprite, string str = "")
    {
        bombImage.sprite = sprite;
        bombCountText.text = str;
    }

    public void SetScoreText(int newScore)
    {
        currentScore = newScore;
    }

    public void SetDropBombText(int value)
    {
        bombDropText.text = value.ToString();
    }

    void Update()
    {
        displayScore = Mathf.Lerp(displayScore, currentScore, scoreSpeed * Time.deltaTime);
        scoreText.text = displayScore.ToString("0");
    }
}
