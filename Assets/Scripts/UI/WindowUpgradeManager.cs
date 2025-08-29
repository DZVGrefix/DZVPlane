using UnityEngine;

public class WindowUpgradeManager : MonoBehaviour
{
    [SerializeField] UpgradeButton[] upgradeButtons;

    UpgradeDM _upgradeDM;
    PlayerDM _playerDM;
    PlayerData _playerData;

    public void Setup()
    {
        _playerDM = PlayerDM.Instance();
        _playerData = _playerDM.GetPlayerData();

        _upgradeDM = UpgradeDM.Instance();
        _upgradeDM.Setup();
        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            upgradeButtons[i].Setup(_upgradeDM, _upgradeDM.GetUpgradeRowWithIndex(i), i, _playerData);
        }
    }

    void SetPlayerDataWithUpgrade(UpgradeRow row) {
        switch (row.upgradetype)
        {
            case UpgradeType.PlusDropBomb:
                _playerData.PlusDropBomb = (int)row.value;
                break;
            case UpgradeType.BombFallSpeed:
                _playerData.BombFallSpeed = row.value;
                break;
            case UpgradeType.PlusBombDmg:
                _playerData.PlusBombDmg = (int)row.value;
                break;
            case UpgradeType.PlusShrapnelDmg:
                _playerData.PlusShrapnelDmg = (int)row.value;
                break;
            case UpgradeType.PlaneSpeedReduction:
                _playerData.PlaneSpeedReduction = row.value;
                break;
            case UpgradeType.MoreBomb:
                _playerData.MoreBomb = (int)row.value;
                break;
            case UpgradeType.FreezeHeight:
                _playerData.FreezeHeight = (int)row.value;
                break;
        }
    }

    public void ClickUpgradeButton(UpgradeRow row, int index)
    {
        //meg kell nézni hogy van e elég pénzem erre.
        if (_playerData.coins < row.buyCoins) return;

        _playerData.coins -= row.buyCoins;
        // menteni kell a playet datát
#warning "Meg felelő helyen menteni kell a playerdatában az upgradet"
        SetPlayerDataWithUpgrade(row);
        PlayerDM.Instance().SetData(_playerData, 0);

        //átállítom a gombot megvásároltra
        row.shoptype = ShopType.Complate;
        //ComplateItem();
        upgradeButtons[index].RefresButtonUI(row.shoptype);
        _upgradeDM.SetData(row, index);

        //meg nézem hogy a következő gomb fel van-e oldva ha igen változtatom a státuszát.
        UpgradeRow[] upgradeRows = _upgradeDM.GetUpgradeRowWithType(row.upgradetype);
        // megnézem hanyadik vagyok a sorban
        int i = _upgradeDM.ContainsAtIndex(upgradeRows, row);
        if (i + 1 < upgradeRows.Length)
        {
            UpgradeRow nextRow = upgradeRows[i + 1];
            if (nextRow.shoptype == ShopType.LockBuy)
            {
                nextRow.shoptype = ShopType.Buy;
                //BuyItem();
                upgradeButtons[index + 1].RefresButtonUI(nextRow.shoptype);
                _upgradeDM.SetData(nextRow, index + 1);
            }
        }
        //_upgradeDM.SetData(row, index);
    }
}
