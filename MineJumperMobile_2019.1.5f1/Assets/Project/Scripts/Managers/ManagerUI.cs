using Cards;
using Core;
using Events;
using System;
using System.Collections;
using UnityEngine;

namespace Managers {

    public class ManagerUI : Base {

        public enum ButtonRole {
            PlayButton, ExitButton, MenuButton
        }

        private bool _isPaused;

        #region Inspector

        [SerializeField] private GameObject _menuPanel;
        [SerializeField] private UnityEngine.UI.Slider _sizeSlider;
        [SerializeField] private TMPro.TMP_Text _sizeText;
        [SerializeField] private UnityEngine.UI.Slider _bombsSlider;
        [SerializeField] private TMPro.TMP_Text _bombsText;
        [SerializeField] private Camera _depthCamera;

        [SerializeField] private GameObject _gamePanel;
        [SerializeField] private TMPro.TMP_Text _bombsLeftText;
        [SerializeField] private TMPro.TMP_Text _pauseButtonText;

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
            _menuPanel.SetActive(true);
            _gamePanel.SetActive(false);
            _gameOverPanel.SetActive(false);
        }

        public void ShowGameUI() {
            _menuPanel.SetActive(false);
            _gamePanel.SetActive(true);
            _gameOverPanel.SetActive(false);
            _bombsLeftText.text = Bombs.ToString();
        }

        public void ShowGameOverUI(BoardStatus status) {
            switch (status) {
                case BoardStatus.Lose:
                    _statusText.text = "LOSE";
                    break;
                case BoardStatus.Win:
                    _statusText.text = "WIN";
                    break;
            }
            _menuPanel.SetActive(false);
            _gamePanel.SetActive(false);
            _gameOverPanel.SetActive(true);
        }

        #endregion

        #region UI Handlers

        public void OnMenuButtonClick(bool fromPlayMode) {
            if (!_isPaused) {
                ClearOldGame();
                _depthCamera.enabled = !fromPlayMode;
                ButtonClicked(ButtonRole.MenuButton);
                ShowMenuUI();
            }
        }

        public void OnPlayButtonClick() {
            ClearOldGame();
            _depthCamera.enabled = false;
            ButtonClicked(ButtonRole.PlayButton);
            ShowGameUI();
        }

        private void ClearOldGame() {
            foreach (var item in FindObjectsOfType<GameCard>()) {
                Destroy(item.gameObject);
            }

            var markingCard = FindObjectOfType<MarkingCard>();
            if (markingCard)
                Destroy(markingCard.gameObject);
        }

        public void OnExitButtonClick() {
            StartCoroutine(QuitRoutine());
        }

        public void OnSizeSliderValueChanged() {
            if (_sizeText) {
                if (_sizeSlider) {
                    Size = (int)_sizeSlider.value;
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
                    Bombs = (int)_bombsSlider.value;
                    _bombsText.text = _bombsSlider.value.ToString();
                }
                else {
                    DebugMessage.LogNotSetupWarningMessage("BombsSlider");
                }
            }
            else
                DebugMessage.LogNotSetupWarningMessage("BombsText");
        }

        public void TogglePause() {
            _isPaused = !_isPaused;
            _pauseButtonText.color = _isPaused ? Color.green : Color.white;
            Time.timeScale = 1 - Time.timeScale;
        }

        public bool ChangeBombsLeft(int delta) {
            if (Bombs + delta < 0)
                return false;
            Bombs += delta;
            _bombsLeftText.text = Bombs.ToString();
            return true;
        }

        #endregion

        private IEnumerator QuitRoutine() {
            ButtonClicked(ButtonRole.ExitButton);
            yield return new WaitForSeconds(0.5f);
            Application.Quit();
        }
    }
}
