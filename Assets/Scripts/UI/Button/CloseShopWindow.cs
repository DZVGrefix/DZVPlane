public class CloseShopWindow : ButtonBehaviour<LevelNavigationManager>
{
    protected override void OnClick()
    {
        Manager.GoBackToLevelWindow();
    }
}
