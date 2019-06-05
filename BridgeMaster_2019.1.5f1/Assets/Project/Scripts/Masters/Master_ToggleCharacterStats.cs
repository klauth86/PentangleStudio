using BridgeMaster.Dicts;
using UnityEngine;

namespace BridgeMaster.Masters {
    public class Master_ToggleCharacterStats : Base {
        [SerializeField] private GameObject _characterStats;

        private void OnEnable() {
            Master.InputKeyEvent += ToggleCharacterStats;
        }

        private void OnDisable() {
            Master.InputKeyEvent -= ToggleCharacterStats;
        }

        private void ToggleCharacterStats(InputAction action) {
            if (action == InputAction.ToggleCharacterStatsAction) {
                _characterStats.SetActive(!_characterStats.activeSelf);
                Master.ToggleCharacterStats();
            }
        }
    }
}