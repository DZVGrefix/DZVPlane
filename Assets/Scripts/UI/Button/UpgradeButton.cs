using System.Linq;
using TMPro;
using UnityEngine;

public class UpgradeButton : ButtonBehaviour<WindowUpgradeManager>
{
    [SerializeField] GameObject lockObj;
    [SerializeField] TMP_Text lockText;
    [SerializeField] GameObject buyObj;
    [SerializeField] TMP_Text buyText;
    [SerializeField] GameObject lockBuyObj;
    [SerializeField] GameObject complateObj;
    

    int _indexRow;
    UpgradeDM _upgradeDM;
    UpgradeRow _upgradeRow;
    PlayerData _playerData;

    
    public void Setup(UpgradeDM upgradeDM, UpgradeRow row, int index, PlayerData playerData)
    {
        _upgradeDM = upgradeDM;
        _upgradeRow = row;
        _indexRow = index;
        _playerData = playerData;

        RefresButtonUI(GetUpgradeType());
    }

    public void RefresButtonUI(ShopType type)
    {
        switch (type)
        {
            case ShopType.Lock:
                {
                    LockItem();
                    break;
                }
            case ShopType.Buy:
                {
                    BuyItem();
                    break;
                }
            case ShopType.LockBuy:
                {
                    LockBuyItem();
                    break;
                }
            case ShopType.Complate:
                {
                    ComplateItem();
                    break;
                }
        }
    }

    ShopType GetUpgradeType() {
        if (_upgradeRow.shoptype == ShopType.Complate) return _upgradeRow.shoptype;

        // ha lock státuszban van akkor vagy Buy a státusza vagy BuyLock
        // buy akkor van ha vagy az első elem vagy az előtte lévő elem már complate.
        // buyLock akkor van ha az előtte lévő elem még nincs megvásárolva

        if (_playerData.level >= _upgradeRow.lockLvl)
        {
            // meg van az az adott elem
            // le szür az adat bázis erre a tipusra.
            UpgradeRow[] upgradeRows = _upgradeDM.GetUpgradeRowWithType(_upgradeRow.upgradetype);
            // megnézem hanyadik vagyok a sorban
            int i = _upgradeDM.ContainsAtIndex(upgradeRows, _upgradeRow);
            // ha minusz egy akkor valami gond van vissza térek a megléő adattal
            if (i == -1) return _upgradeRow.shoptype;

            if (i == 0)
            {
                _upgradeRow.shoptype = ShopType.Buy;
            }
            else
            {
                if (upgradeRows[i - 1].shoptype == ShopType.Complate)
                {
                    _upgradeRow.shoptype = ShopType.Buy;
                }
                else
                {
                    _upgradeRow.shoptype = ShopType.LockBuy;
                }
            }
            _upgradeDM.SetData(_upgradeRow, _indexRow);
        }
        return _upgradeRow.shoptype;
    }

    

    void LockItem()
    {
        lockObj.SetActive(true);
        buyObj.SetActive(false);
        lockBuyObj.SetActive(false);
        complateObj.SetActive(false);
        lockText.text = $"Level {_upgradeRow.lockLvl}";
        MyButton.interactable = false;
    }

    void BuyItem()
    {
        lockObj.SetActive(false);
        buyObj.SetActive(true);
        lockBuyObj.SetActive(false);
        complateObj.SetActive(false);
        buyText.text = $"{_upgradeRow.buyCoins}";
        MyButton.interactable = true;
    }

    void LockBuyItem() {
        lockObj.SetActive(false);
        buyObj.SetActive(false);
        lockBuyObj.SetActive(true);
        complateObj.SetActive(false);
        buyText.text = $"{_upgradeRow.buyCoins}";
        MyButton.interactable = false;
    }

    void ComplateItem()
    {
        lockObj.SetActive(false);
        buyObj.SetActive(false);
        lockBuyObj.SetActive(false);
        complateObj.SetActive(true);
        MyButton.interactable = false;
    }

    protected override void OnClick()
    {
        Manager.ClickUpgradeButton(_upgradeRow, _indexRow);
    }
}

