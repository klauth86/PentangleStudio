using Core;
using Events;
using UnityEngine;

namespace Managers {

    public class ManagerUI : Base {

        public enum ButtonRole {
            PlayButton, ExitButton
        }

        #region Inspector

        [SerializeField] private GameObject _menuPanel;

        [SerializeField] private GameObject _gamePanel;
        [SerializeField] private TMPro.TMP_Text _bombsLeftText;

        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private TMPro.TMP_Text _statusText;

        [SerializeField] private GameObject _playButton;

        #endregion

        public int Size;
        public int Bombs;

        #region Events

        public event GameAction<ButtonRole> ButtonClicked = delegate { };

        #endregion

        #region Manager Methods

        public void ShowMenuUI() {

        }

        public void ShowGameUI() {

        }

        public void ShowGameOverUI(BoardStatus status) {
            string text = "";
            switch (status) {
                case BoardStatus.Lose:
                    text = "LOSE !!!";
                    break;
                case BoardStatus.Win:
                    text = "WIN !!!";
                    break;
            }
        }

        #endregion

        #region UI Handlers

        public void OnPlayButtonClick() {
            ButtonClicked(ButtonRole.PlayButton);
            ShowGameUI();
        }

        public void OnExitButtonClick() {
            ButtonClicked(ButtonRole.ExitButton);
            Application.Quit();
        }

        #endregion
    }

}
