public class OpenUpgradeWindow : ButtonBehaviour<LevelNavigationManager>
{
    protected override void OnClick()
    {
        Manager.ShowUpgradeWindow();
    }
}
