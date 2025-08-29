using UnityEngine;

public class ShopChangePlaneButton : ButtonBehaviour<WindowShopManager>
{
    [SerializeField] bool isPlaneAcite;
    protected override void OnClick()
    {
        Manager.ChangeShopButton(isPlaneAcite);
    }
}
