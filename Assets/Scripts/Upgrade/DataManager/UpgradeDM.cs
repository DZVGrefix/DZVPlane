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
            new(shoptype: ShopType.Lock, upgradetype: UpgradeType.PlusDropBomb, value: 1, lockLvl: 5, buyCoins: 100),
            new(shoptype: ShopType.Lock, upgradetype: UpgradeType.PlusDropBomb, value: 2, lockLvl: 15, buyCoins: 1000),
            new(shoptype: ShopType.Lock, upgradetype: UpgradeType.PlusDropBomb, value: 3, lockLvl: 20, buyCoins: 2000),
            new(shoptype: ShopType.Lock, upgradetype: UpgradeType.BombFallSpeed, value: 1.05f, lockLvl: 5, buyCoins: 100),
            new(shoptype: ShopType.Lock, upgradetype: UpgradeType.BombFallSpeed, value: 1.10f, lockLvl: 15, buyCoins: 1000),
            new(shoptype: ShopType.Lock, upgradetype: UpgradeType.BombFallSpeed, value: 1.15f, lockLvl: 20, buyCoins: 2000),
            new(shoptype: ShopType.Lock, upgradetype: UpgradeType.PlusBombDmg, value: 1, lockLvl: 5, buyCoins: 100),
            new(shoptype: ShopType.Lock, upgradetype: UpgradeType.PlusBombDmg, value: 2, lockLvl: 15, buyCoins: 1000),
            new(shoptype: ShopType.Lock, upgradetype: UpgradeType.PlusBombDmg, value: 3, lockLvl: 20, buyCoins: 2000),
            new(shoptype: ShopType.Lock, upgradetype: UpgradeType.PlusShrapnelDmg, value: 1, lockLvl: 5, buyCoins: 100),
            new(shoptype: ShopType.Lock, upgradetype: UpgradeType.PlusShrapnelDmg, value: 2, lockLvl: 15, buyCoins: 1000),
            new(shoptype: ShopType.Lock, upgradetype: UpgradeType.PlusShrapnelDmg, value: 3, lockLvl: 20, buyCoins: 2000),
            new(shoptype: ShopType.Lock, upgradetype: UpgradeType.PlaneSpeedReduction, value: 0.95f, lockLvl: 5, buyCoins: 100),
            new(shoptype: ShopType.Lock, upgradetype: UpgradeType.PlaneSpeedReduction, value: 0.9f, lockLvl: 15, buyCoins: 1000),
            new(shoptype: ShopType.Lock, upgradetype: UpgradeType.PlaneSpeedReduction, value: 0.85f, lockLvl: 20, buyCoins: 2000),
            new(shoptype: ShopType.Lock, upgradetype: UpgradeType.MoreBomb, value: 1, lockLvl: 5, buyCoins: 100),
            new(shoptype: ShopType.Lock, upgradetype: UpgradeType.MoreBomb, value: 2, lockLvl: 15, buyCoins: 1000),
            new(shoptype: ShopType.Lock, upgradetype: UpgradeType.MoreBomb, value: 3, lockLvl: 20, buyCoins: 2000),
            new(shoptype: ShopType.Lock, upgradetype: UpgradeType.FreezeHeight, value: 1, lockLvl: 5, buyCoins: 100),
            new(shoptype: ShopType.Lock, upgradetype: UpgradeType.FreezeHeight, value: 2, lockLvl: 15, buyCoins: 1000),
            new(shoptype: ShopType.Lock, upgradetype: UpgradeType.FreezeHeight, value: 3, lockLvl: 20, buyCoins: 2000),
        };
    }

    
}
