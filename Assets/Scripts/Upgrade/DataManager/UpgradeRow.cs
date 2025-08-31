[System.Serializable]
public class UpgradeRow
{
    public ShopType shoptype;                       // shop type (lock, unlock, complate ... )
    public UpgradeType upgradetype;                 // upgrade tipus (bombDMG, bombSpeed ...)
    public float value;                             // érték amit meg veszek
    public int lockLvl;                             // zárolási level szint
    public int buyCoins;                            // mennyibe kerül az item

    public UpgradeRow(ShopType shoptype, UpgradeType upgradetype, float value, int lockLvl, int buyCoins)
    {
        this.shoptype = shoptype;
        this.upgradetype = upgradetype;
        this.value = value;
        this.lockLvl = lockLvl;
        this.buyCoins = buyCoins;
    }
}
