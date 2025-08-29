public class RestartGameOverButton : ButtonBehaviour<GameNavigation>
{
    protected override void OnClick()
    {
        Manager.RestartGameOverButton();
    }
}
