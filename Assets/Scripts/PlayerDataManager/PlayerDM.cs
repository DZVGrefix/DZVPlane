using System.Linq;
using UnityEngine;

public class PlayerDM : DataManager<PlayerData>
{
    #region Singelton
    private static PlayerDM _instance;
    public static PlayerDM Instance()
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

    [Header("Player DM settings")]
    [SerializeField] PlayerData _playerData;
    const string PlayerKey = "PlayerKey";


    // kezdő beállítások
    public void Setup()
    {
        Load(PlayerKey, _playerData);
        _playerData = datas[0];
    }

    // Player adatok visszaadása
    public PlayerData GetPlayerData()
    {
        return _playerData;
    }

    // Level socre hozzáadása addig nem adom hozzá amig nincs kész az adaott pálya.
    private void AddLevelScore()
    {
        LevelScore levelScore = new()
        {
            highScore = 0,
            score = 0
        };
        _playerData.levelScores.Add(levelScore);
    }

    // le kérdezem hogy melyik adatokra rögzítem a pontszámokat
    public LevelScore GetLevelScoreWithLevel(int index)
    {
        if (index == _playerData.levelScores.Count)
        {
            AddLevelScore();
        }
        return _playerData.levelScores[Mathf.Clamp(index, 0, _playerData.levelScores.Count - 1)];
    }

    // ha modosítom az adatokat utána el is mentem
    public override void SetData(PlayerData value, int index)
    {
        datas[index] = value;
        _playerData = value;
        Save(PlayerKey);
    }
}
