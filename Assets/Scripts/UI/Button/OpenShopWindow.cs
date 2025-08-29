public class OpenShopWindow : ButtonBehaviour<LevelNavigationManager>
{
    protected override void OnClick()
    {
        Manager.ShowShopWindow();
    }
}
