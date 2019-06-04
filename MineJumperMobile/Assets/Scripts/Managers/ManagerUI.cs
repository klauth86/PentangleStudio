using Core;
using Events;
using System.Collections;
using UnityEngine;

namespace Managers {

    public class ManagerUI : Base {

        public enum ButtonRole {
            PlayButton, ExitButton
        }

        #region Inspector

        [SerializeField] private GameObject _menuPanel;
        [SerializeField] private UnityEngine.UI.Slider _sizeSlider;
        [SerializeField] private TMPro.TMP_Text _sizeText;
        [SerializeField] private UnityEngine.UI.Slider _bombsSlider;
        [SerializeField] private TMPro.TMP_Text _bombsText;

        [SerializeField] private GameObject _gamePanel;
        [SerializeField] private TMPro.TMP_Text _bombsLeftText;

        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private TMPro.TMP_Text _statusText;

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
            StartCoroutine(QuitRoutine());
        }

        public void OnSizeSliderValueChanged() {
            if (_sizeText) {
                if (_sizeSlider) {
                    _sizeText.text = _sizeSlider.value.ToString();
                }
                else {
                    DebugMessage.LogNotSetupWarningMessage("SizeSlider");
                }
            }               
            else
                DebugMessage.LogNotSetupWarningMessage("SizeText");
        }

        public void OnBombsSliderValueChanged() {
            if (_bombsText) {
                if (_bombsSlider) {
                    _bombsText.text = _bombsSlider.value.ToString();
                }
                else {
                    DebugMessage.LogNotSetupWarningMessage("BombsSlider");
                }
            }
            else
                DebugMessage.LogNotSetupWarningMessage("BombsText");
        }

        #endregion

        private IEnumerator QuitRoutine() {
            ButtonClicked(ButtonRole.ExitButton);
            yield return new WaitForSeconds(0.5f);
            Application.Quit();
        }
    }

}
