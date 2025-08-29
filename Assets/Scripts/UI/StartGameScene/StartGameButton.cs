public class StartGameButton : ButtonBehaviour<StartGameNavigationManager>
{
    protected override void OnClick()
    {
        Manager.StartGameButtonClick();
    }
}
