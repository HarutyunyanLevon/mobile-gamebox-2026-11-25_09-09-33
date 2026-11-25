using Gamebox.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gamebox.App
{
    public sealed class ModuleTile : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private Image _icon;

        public void Bind(ModuleDefinition definition, ISceneNavigator navigator)
        {
            _title.text = definition.Title;

            // No icon yet keeps the prefab's placeholder look.
            if (definition.Icon != null)
            {
                _icon.sprite = definition.Icon;
            }

            _button.onClick.AddListener(() => navigator.OpenModule(definition.EntrySceneAddress));
        }
    }
}
