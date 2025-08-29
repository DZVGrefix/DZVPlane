public class ShopItemNextButton : ButtonBehaviour<WindowShopManager>
{
    protected override void OnClick()
    {
        Manager.NextButton();
    }
}
