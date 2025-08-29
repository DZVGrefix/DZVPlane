public class CloseUpgradeWindow : ButtonBehaviour<LevelNavigationManager>
{
    protected override void OnClick()
    {
        Manager.GoBackToLevelWindow();
    }
}
