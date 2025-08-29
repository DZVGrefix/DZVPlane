using TMPro;
using UnityEngine;
using UnityEngine.UI;


/*
    át kell írni ugy hogy a bomba vezérlésre is jó legyen
    létrehozok egy WindowsShopGUI-t oda teszek minden TMP-t
    csinálok egy planeshopcontroller-t ami lekéri WindowShopGUI komponenst és át adja ShopPlane-t és az alapján megjelenik.
    csinálok egy Bombshopcontroller-t is ami ugyanazt csinálja
    és kell egy ShopNavigator amire befutnak a nyomo gombok
    - egy bool változo hogy mikor melyik
    - pl indulás melyik adatokat tölti be.
    - next melyik controller nextje kell

    majd egy gombok ami vált a két manager közt.
*/
[RequireComponent(typeof(WindowShopGUI))]
public class WindowShopManager : MonoBehaviour
{
    ShopPlaneDM _shopPlaneDM;
    int _indexPlane;
    ShopBombDM _shopBombDM;
    int _indexBomb;
    bool _isPlaneAcite;
    int _count;
    int _level;
    WindowShopGUI _windowShopGUI;


    public void Setup()
    {
        //PlayerDM.Instance().Setup();
        PlayerData playerData = PlayerDM.Instance().GetPlayerData();
        _level = playerData.level;

        _shopPlaneDM = ShopPlaneDM.Instance();
        _indexPlane = 0;

        _shopBombDM = ShopBombDM.Instance();
        _indexBomb = 0;

        _windowShopGUI = GetComponent<WindowShopGUI>();
        _isPlaneAcite = true;
        PlaneOrBombSetup();
    }

    void PlaneOrBombSetup()
    {
        if (_isPlaneAcite)
        {
            PlaneSetup();
        }
        else
        {
            BombSetup();
        }
    }

    void PlaneSetup()
    {
        _count = _shopPlaneDM.GetShopPlaneCount();
        ShopPlane shopPlane = _shopPlaneDM.GetShopPlaneWithIndex(_indexPlane);
        _windowShopGUI.RefreshPlaneGUI(shopPlane, _level);
    }

    void BombSetup()
    {
        _count = _shopBombDM.GetShopBombCount();
        ShopBomb shopBomb = _shopBombDM.GetShopBombWithIndex(_indexBomb);
        _windowShopGUI.RefreshPlaneGUI(shopBomb, _level);
    }

    public void NextButton()
    {
        if (_isPlaneAcite)
        {
            _indexPlane = Next(_indexPlane);
        }
        else
        {
            _indexBomb = Next(_indexBomb);
        }
        PlaneOrBombSetup();
        //ShopPlane shopPlane = _shopPlaneDM.GetShopPlaneWithIndex(_indexPlane);
        //_windowShopGUI.RefreshPlaneGUI(shopPlane, _level);
    }
    int Next(int value)
    {
        value++;
        value %= _count;
        return value;
    }

    public void PreviousButton()
    {
        if (_isPlaneAcite)
        {
            _indexPlane = Previous(_indexPlane);
        }
        else
        {
            _indexBomb = Previous(_indexBomb);
        }
        PlaneOrBombSetup();
        //ShopPlane shopPlane = _shopPlaneDM.GetShopPlaneWithIndex(_indexPlane);
        //_windowShopGUI.RefreshPlaneGUI(shopPlane, _level);
    }
    int Previous(int value)
    {
        value--;
        if (value < 0)
        {
            value = _count - 1;
        }
        else
        {
            value %= _count;
        }
        return value;
    }

    public void BuyButton()
    {
        if (_isPlaneAcite)
        {
            PlaneBuyButton();
        }
        else
        {
            BombBuyButton();
        }
    }
    void PlaneBuyButton()
    {
        ShopPlane shopPlane = _shopPlaneDM.GetShopPlaneWithIndex(_indexPlane);
        if (shopPlane.shopItem.isUsePlane) return;

        if (shopPlane.shopItem.isBuy)
        {
            // már meg vásároltam csak ezt teszem aktivvá
            _shopPlaneDM.SetActivePlane(_indexPlane);
            _windowShopGUI.SetPlaneButtonText((shopPlane.shopItem.isUsePlane ? "Active" : "Use default"));
        }
        else
        {
            // ha van rá pénzem meg veszem
            //PlayerDM.Instance().Setup();
            PlayerData playerData = PlayerDM.Instance().GetPlayerData();
            if (shopPlane.shopItem.price < playerData.coins)
            {
                // van elég pénzem.
                playerData.coins -= shopPlane.shopItem.price;
                PlayerDM.Instance().SetData(playerData, 0);
                shopPlane.shopItem.isBuy = true;
                _shopPlaneDM.SetData(shopPlane.shopItem, _indexPlane);
                _windowShopGUI.SetPlaneButtonText((shopPlane.shopItem.isUsePlane ? "Active" : "Use default"));
            }
            else
            {
                _windowShopGUI.SetPlaneButtonText($"{shopPlane.shopItem.price}");
            }
        }
    }

    void BombBuyButton()
    {
        ShopBomb shopBomb = _shopBombDM.GetShopBombWithIndex(_indexPlane);
        if (!shopBomb.shopItem.isBuy)
        {
            PlayerData playerData = PlayerDM.Instance().GetPlayerData();
            if (shopBomb.shopItem.price < playerData.coins)
            {
                // van elég pénzem
                playerData.coins -= shopBomb.shopItem.price;
                PlayerDM.Instance().SetData(playerData, 0);
                shopBomb.shopItem.isBuy = true;
                _shopBombDM.SetData(shopBomb.shopItem, _indexBomb);
                _windowShopGUI.SetBombButtonText("", false);
            }
            else
            {
                _windowShopGUI.SetBombButtonText($"{shopBomb.shopItem.price}");
            }
        }
    }

    public void ChangeShopButton(bool isPlaneAcite)
    {
        _isPlaneAcite = isPlaneAcite;
        PlaneOrBombSetup();
    }
}
