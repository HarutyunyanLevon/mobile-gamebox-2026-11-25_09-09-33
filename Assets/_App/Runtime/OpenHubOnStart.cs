using Gamebox.Core;
using UnityEngine;
using VContainer;

namespace Gamebox.App
{
    // Sits in the Boot scene, the only build scene; everything after it loads through Addressables.
    public sealed class OpenHubOnStart : MonoBehaviour
    {
        private ISceneNavigator _navigator;

        [Inject]
        public void Construct(ISceneNavigator navigator) => _navigator = navigator;

        private void Start() => _navigator.OpenHub();
    }
}
