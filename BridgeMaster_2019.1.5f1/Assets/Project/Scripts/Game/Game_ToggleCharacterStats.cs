using BridgeMaster.Dicts;
using UnityEngine;

namespace BridgeMaster.Game {
    public class Game_ToggleCharacterStats : ComponentSubscriber<Master> {
        [SerializeField] private GameObject _characterStats;

        private void OnEnable() {
            Master.InputKeyEvent += ToggleCharacterStats;
        }

        private void OnDisable() {
            Master.InputKeyEvent -= ToggleCharacterStats;
        }

        private void ToggleCharacterStats(InputAction action, InputActionState state, float axis) {
            if (action == InputAction.ToggleCharacterStatsAction) {
                _characterStats.SetActive(!_characterStats.activeSelf);
                Master.ToggleCharacterStats();
            }
        }
    }
}