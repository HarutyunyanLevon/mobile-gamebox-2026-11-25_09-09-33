using UnityEngine;

namespace Gamebox.Core
{
    // The hub builds a tile from each of these, so adding a module never touches hub code.
    [CreateAssetMenu(menuName = MenuName)]
    public sealed class ModuleDefinition : ScriptableObject
    {
        private const string MenuName = "Gamebox/Module Definition";

        [SerializeField] private string _id;
        [SerializeField] private string _title;
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _entrySceneAddress;
        [SerializeField] private bool _enabled = true;

        public string Id => _id;
        public string Title => _title;
        public Sprite Icon => _icon;
        public string EntrySceneAddress => _entrySceneAddress;
        public bool Enabled => _enabled;
    }
}
