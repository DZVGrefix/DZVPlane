public class CloseLevelWindow : ButtonBehaviour<LevelNavigationManager>
{
    protected override void OnClick()
    {
        Manager.GoBackToStartWindow();
    }
}
