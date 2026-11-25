using Gamebox.Core;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Gamebox.UI
{
    public sealed class BackButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        [Inject]
        public void Construct(ISceneNavigator navigator) => _button.onClick.AddListener(navigator.OpenHub);
    }
}
