using System.Collections;
using UnityEngine;

namespace MineJumperMobile_2019.Masters {
    class SubUI: Sub {

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

        private void ShowMenuUI() {
            _gamePanel.SetActive(false);
            _gameOverPanel.SetActive(false);

            _menuPanel.SetActive(true);
        }

        private void ShowGameUI() {
            _menuPanel.SetActive(false);
            _gameOverPanel.SetActive(false);

            _gamePanel.SetActive(true);
        }

        private void ShowGameOverUI(string statusText) {
            _menuPanel.SetActive(false);
            _gamePanel.SetActive(false);

            _statusText.text = statusText;
            _gameOverPanel.SetActive(true);
        }

        #region UI Handlers

        public void OnMenuButtonClick(bool fromPlayMode) {
            if (!_isPaused) {
                Master.CallButtonActionEvent(Dicts.ButtonAction.MenuButtonAction);
                _depthCamera.enabled = !fromPlayMode;
                ShowMenuUI();
            }
        }

        public void OnPlayButtonClick() {
            Master.CallButtonActionEvent(Dicts.ButtonAction.PlayButtonAction);
            _depthCamera.enabled = false;
            ShowGameUI();
        }

        public void OnExitButtonClick() {
            StartCoroutine(QuitRoutine());
        }

        public void OnSizeSliderValueChanged() {
            if (_sizeText) {
                if (_sizeSlider) {
                    Master.CallSizeChangedEvent((int)_sizeSlider.value);
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
                    Master.CallBombsChangedEvent((int)_bombsSlider.value);
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

        #endregion

        private IEnumerator QuitRoutine() {
            Master.CallButtonActionEvent(Dicts.ButtonAction.ExitButtonAction);
            yield return new WaitForSeconds(0.5f);
            Application.Quit();
        }

        private void OnEnable() {
            Master.BombsLeftChangedEvent += OnBombsLeftChangedEvent;
            Master.GameOverEvent += OnGameOverEvent;
        }

        private void OnDisable() {
            Master.BombsLeftChangedEvent -= OnBombsLeftChangedEvent;
            Master.GameOverEvent -= OnGameOverEvent;
        }

        private void OnGameOverEvent(bool win) {
            ShowGameOverUI(win ? "WIN" : "LOSE");
        }

        private void OnBombsLeftChangedEvent(int bombsLeft) {
            _bombsLeftText.text = bombsLeft.ToString();
        }
    }
}
