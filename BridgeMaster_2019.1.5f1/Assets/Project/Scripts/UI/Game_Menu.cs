using BridgeMaster.Dicts;
using BridgeMaster.Game;
using UnityEngine;

namespace BridgeMaster.UI {
    public class Game_Menu : MonoBehaviour {
        [SerializeField] private GameObject _menu;

        private void OnEnable() {
            Game_Master.Instance.InputKeyEvent += ToggleMenu;
        }

        private void OnDisable() {
            Game_Master.Instance.InputKeyEvent -= ToggleMenu;
        }

        private void ToggleMenu(InputActions action, InputActionStates state, float axis) {
            if (action == InputActions.ToggleMenuAction) {
                _menu.SetActive(!_menu.activeSelf);
                Game_Master.Instance.ToggleMenu(_menu.activeSelf);
            }
        }
    }
}