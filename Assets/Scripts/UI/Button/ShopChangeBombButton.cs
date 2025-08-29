using UnityEngine;

public class ShopChangeBombButton : ButtonBehaviour<WindowShopManager>
{
    [SerializeField] bool isPlaneAcite;
    protected override void OnClick()
    {
        Manager.ChangeShopButton(isPlaneAcite);
    }
}
