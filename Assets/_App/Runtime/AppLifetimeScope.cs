using Gamebox.Core;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;
using VContainer.Unity;

namespace Gamebox.App
{
    // Root scope: VContainerSettings spawns it once and keeps it across scene loads.
    public sealed class AppLifetimeScope : LifetimeScope
    {
        [SerializeField] private AssetReference _hubScene;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<ISceneNavigator, SceneNavigator>(Lifetime.Singleton).WithParameter(_hubScene);
        }
    }
}
