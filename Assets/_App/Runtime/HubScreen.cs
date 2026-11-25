using System.Collections.Generic;
using Gamebox.Core;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using VContainer;

namespace Gamebox.App
{
    // Finds modules by Addressables label, so a new module only needs its definition labelled.
    public sealed class HubScreen : MonoBehaviour
    {
        [SerializeField] private AssetLabelReference _moduleLabel;
        [SerializeField] private ModuleTile _tilePrefab;
        [SerializeField] private Transform _tileParent;

        private ISceneNavigator _navigator;
        private AsyncOperationHandle<IList<ModuleDefinition>> _definitions;

        [Inject]
        public void Construct(ISceneNavigator navigator) => _navigator = navigator;

        private void Start()
        {
            _definitions = Addressables.LoadAssetsAsync<ModuleDefinition>(_moduleLabel, null);
            _definitions.Completed += ShowTiles;
        }

        private void OnDestroy()
        {
            if (_definitions.IsValid())
            {
                _definitions.Release();
            }
        }

        private void ShowTiles(AsyncOperationHandle<IList<ModuleDefinition>> handle)
        {
            if (handle.Status != AsyncOperationStatus.Succeeded)
            {
                Debug.LogError($"Hub could not load module definitions: {handle.OperationException}");
                return;
            }

            foreach (ModuleDefinition definition in ModuleFilter.Shown(handle.Result))
            {
                Instantiate(_tilePrefab, _tileParent).Bind(definition, _navigator);
            }
        }
    }
}
