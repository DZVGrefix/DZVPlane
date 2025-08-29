public class QuitGameButton : ButtonBehaviour<StartGameNavigationManager>
{
    protected override void OnClick()
    {
        Manager.QuitGameButtonClick();
    }
}
