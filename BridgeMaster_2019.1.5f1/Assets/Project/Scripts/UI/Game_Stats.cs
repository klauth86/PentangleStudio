using BridgeMaster.Dicts;
using BridgeMaster.Game;
using UnityEngine;

namespace BridgeMaster.UI {
    public class Game_Stats : MonoBehaviour {
        [SerializeField] private GameObject _characterStats;

        private void OnEnable() {
            Game_Master.Instance.InputKeyEvent += ToggleCharacterStats;
        }

        private void OnDisable() {
            Game_Master.Instance.InputKeyEvent -= ToggleCharacterStats;
        }

        private void ToggleCharacterStats(InputActions action, InputActionStates state, float axis) {
            if (action == InputActions.ToggleCharacterStatsAction) {
                _characterStats.SetActive(!_characterStats.activeSelf);
                Game_Master.Instance.ToggleCharacterStats(_characterStats.activeSelf);
            }
        }
    }
}