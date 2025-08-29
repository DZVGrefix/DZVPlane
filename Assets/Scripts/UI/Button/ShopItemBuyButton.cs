using UnityEngine;

public class ShopItemBuyButton : ButtonBehaviour<WindowShopManager>
{
    protected override void OnClick()
    {
        Manager.BuyButton();
    }
}
