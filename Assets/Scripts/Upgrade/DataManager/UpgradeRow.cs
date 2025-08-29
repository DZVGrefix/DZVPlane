[System.Serializable]
public class UpgradeRow
{
    public ShopType shoptype;
    public UpgradeType upgradetype;
    public float value = 0f;
    public int lockLvl = 0;
    public int buyCoins = 0;

    public UpgradeRow(ShopType shop, UpgradeType upgrade, float value, int lockLvl, int buyCoins)
    {
        this.shoptype = shop;
        this.upgradetype = upgrade;
        this.value = value;
        this.lockLvl = lockLvl;
        this.buyCoins = buyCoins;
    }
}
