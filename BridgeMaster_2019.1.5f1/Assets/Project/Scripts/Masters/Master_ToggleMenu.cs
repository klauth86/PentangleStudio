using UnityEngine;

namespace BridgeMaster.Masters {
    public class Master_ToggleMenu : Base {
        [SerializeField] private GameObject _menu;

        private void OnEnable() {
            Master.InputKeyEvent += ToggleMenu;
        }

        private void OnDisable() {
            Master.InputKeyEvent -= ToggleMenu;
        }

        private void ToggleMenu(InputAction action) {
            if (action == InputAction.ToggleMenuAction) {
                _menu.SetActive(!_menu.activeSelf);
                Master.ToggleMenu();
            }
        }
    }
}