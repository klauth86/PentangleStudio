using BridgeMaster.Dicts;
using UnityEngine;

namespace BridgeMaster.Game {
    public class Game_ToggleMenu : Game_Base {
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