using BridgeMaster.Dicts;
using BridgeMaster.Game;
using UnityEngine;

namespace BridgeMaster.UI {
    public class Game_Menu : MonoBehaviour {
        [SerializeField] private GameObject _menu;

        private void OnEnable() {
            Master.Instance.InputKeyEvent += ToggleMenu;
        }

        private void OnDisable() {
            Master.Instance.InputKeyEvent -= ToggleMenu;
        }

        private void ToggleMenu(InputAction action, InputActionState state, float axis) {
            if (action == InputAction.ToggleMenuAction) {
                _menu.SetActive(!_menu.activeSelf);
                Master.Instance.ToggleMenu(_menu.activeSelf);
            }
        }
    }
}