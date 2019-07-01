using BridgeMaster.Dicts;
using BridgeMaster.Game;
using UnityEngine;

namespace BridgeMaster.UI {
    public class Game_Stats : MonoBehaviour {
        [SerializeField] private GameObject _characterStats;

        private void OnEnable() {
            Master.Instance.InputKeyEvent += ToggleCharacterStats;
        }

        private void OnDisable() {
            Master.Instance.InputKeyEvent -= ToggleCharacterStats;
        }

        private void ToggleCharacterStats(InputAction action, InputActionState state, float axis) {
            if (action == InputAction.ToggleCharacterStatsAction) {
                _characterStats.SetActive(!_characterStats.activeSelf);
                Master.Instance.ToggleCharacterStats(_characterStats.activeSelf);
            }
        }
    }
}