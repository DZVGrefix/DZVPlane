public class CancelGameOverButton : ButtonBehaviour<GameNavigation>
{
    protected override void OnClick()
    {
        Manager.CancelGameOverButton();
    }
}
