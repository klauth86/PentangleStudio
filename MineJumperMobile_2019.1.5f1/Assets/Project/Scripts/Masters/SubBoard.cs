using UnityEngine;

namespace MineJumperMobile_2019.Masters {
    public abstract class SubBoard : Sub {

        #region Inspector

        [SerializeField] private GameObject _markingCardPrefab;
        [SerializeField] private GameObject _gameCardPrefab;
        [SerializeField] private float _scaleFactor;

        #endregion

        private void OnEnable() {
            Master.ButtonActionEvent += OnButtonActionEvent;
            Master.GameOverEvent += OnGameOverEvent;
        }

        private void OnDisable() {
            Master.ButtonActionEvent -= OnButtonActionEvent;
            Master.GameOverEvent -= OnGameOverEvent;
        }


        private void OnButtonActionEvent(ButtonAction param) {
        }

        private void OnGameOverEvent(bool win) {
        }
    }
}