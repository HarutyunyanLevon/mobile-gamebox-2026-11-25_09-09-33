using Gamebox.Core;
using UnityEngine.AddressableAssets;

namespace Gamebox.App
{
    // Single-mode loads with the default release mode free the previous scene's handle on unload.
    public sealed class SceneNavigator : ISceneNavigator
    {
        private readonly AssetReference _hubScene;

        public SceneNavigator(AssetReference hubScene) => _hubScene = hubScene;

        public void OpenHub() => Addressables.LoadSceneAsync(_hubScene.RuntimeKey);

        public void OpenModule(string sceneAddress) => Addressables.LoadSceneAsync(sceneAddress);
    }
}
