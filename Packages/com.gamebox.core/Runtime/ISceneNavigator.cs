namespace Gamebox.Core
{
    // Lives in core so games and events can go back to the hub without referencing the app shell.
    public interface ISceneNavigator
    {
        void OpenHub();
        void OpenModule(string sceneAddress);
    }
}
