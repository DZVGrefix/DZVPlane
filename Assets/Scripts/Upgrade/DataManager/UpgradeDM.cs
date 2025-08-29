using System.Linq;
using UnityEngine;

public class UpgradeDM : DataManager<UpgradeRow>
{
    #region Singelton
    private static UpgradeDM _instance;
    public static UpgradeDM Instance()
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

    [Header("Upgrade DM settings")]
    [SerializeField] UpgradeRow[] upgradeRows;
    const string LoadUpgradeKey = "LoadUpgradeKey";

    public void Setup()
    {
        if (upgradeRows == null || upgradeRows.Length == 0)
        {
            CreateUpgradeRows();
        }
        Load(LoadUpgradeKey, upgradeRows);
    }

    public UpgradeRow GetUpgradeRowWithIndex(int index)
    {
        Debug.Log($"UpgradeDM index: {index} tömb mérete: {datas.Count}");
        index = Mathf.Clamp(index, 0, datas.Count - 1);
        return datas[index];
    }

    public UpgradeRow[] GetUpgradeRowWithType(UpgradeType type)
    {
        UpgradeRow[] upgradeRows = datas.Where(x => x.upgradetype == type).ToArray();
        return upgradeRows;
    }
    public int ContainsAtIndex(UpgradeRow[] rows, UpgradeRow row) {
        for (int i = 0; i < rows.Length; i++)
        {
            if (rows[i] == row)
            {
                return i;
            }
        }
        return -1;
    }

    public override void SetData(UpgradeRow value, int index)
    {
        datas[index] = value;
        Save(LoadUpgradeKey);
    }

    void OnValidate()
    {
        CreateUpgradeRows();
    }

    void CreateUpgradeRows()
    {
        upgradeRows = new UpgradeRow[] {
            new(ShopType.Lock, UpgradeType.PlusDropBomb, 1, 5, 100),
            new(ShopType.Lock, UpgradeType.PlusDropBomb, 2, 15, 1000),
            new(ShopType.Lock, UpgradeType.PlusDropBomb, 3, 20, 2000),
            new(ShopType.Lock, UpgradeType.BombFallSpeed, 1.05f, 5, 100),
            new(ShopType.Lock, UpgradeType.BombFallSpeed, 1.10f, 15, 1000),
            new(ShopType.Lock, UpgradeType.BombFallSpeed, 1.15f, 20, 2000),
            new(ShopType.Lock, UpgradeType.PlusBombDmg, 1, 5, 100),
            new(ShopType.Lock, UpgradeType.PlusBombDmg, 2, 15, 1000),
            new(ShopType.Lock, UpgradeType.PlusBombDmg, 3, 20, 2000),
            new(ShopType.Lock, UpgradeType.PlusShrapnelDmg, 1, 5, 100),
            new(ShopType.Lock, UpgradeType.PlusShrapnelDmg, 2, 15, 1000),
            new(ShopType.Lock, UpgradeType.PlusShrapnelDmg, 3, 20, 2000),
            new(ShopType.Lock, UpgradeType.PlaneSpeedReduction, 0.95f, 5, 100),
            new(ShopType.Lock, UpgradeType.PlaneSpeedReduction, 0.9f, 15, 1000),
            new(ShopType.Lock, UpgradeType.PlaneSpeedReduction, 0.85f, 20, 2000),
            new(ShopType.Lock, UpgradeType.MoreBomb, 1, 5, 100),
            new(ShopType.Lock, UpgradeType.MoreBomb, 2, 15, 1000),
            new(ShopType.Lock, UpgradeType.MoreBomb, 3, 20, 2000),
            new(ShopType.Lock, UpgradeType.FreezeHeight, 1, 5, 100),
            new(ShopType.Lock, UpgradeType.FreezeHeight, 2, 15, 1000),
            new(ShopType.Lock, UpgradeType.FreezeHeight, 3, 20, 2000),
        };
    }

    
}
