﻿using BridgeMaster.Dicts;
using UnityEngine;

namespace BridgeMaster.Game {
    public class Game_ToggleCharacterStats : Game_Base {
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