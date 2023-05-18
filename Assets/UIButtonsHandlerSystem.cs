using Client;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Unity.Ugui;
using UnityEngine.Scripting;

namespace UI {

    public sealed class UIButtonsHandlerSystem : EcsUguiCallbackSystem {
        private readonly EcsCustomInject<UIScreen> _screen;

        [Preserve]
        [EcsUguiClickEvent(Idents.Ui.Reset)]
        private void OnResetClicked(in EcsUguiClickEvent e) {
            _screen.Value.ResetCounts();
        }
    }
}