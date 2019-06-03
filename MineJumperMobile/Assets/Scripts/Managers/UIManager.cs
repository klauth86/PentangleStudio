using Base;
using UnityEngine;

namespace Managers {
    public class UIManager : MonoBehaviour {

        [SerializeField] private Game _gamePrefab;

        [SerializeField] private GameObject _menuPanel;

        [SerializeField] private GameObject _gamePanel;
        [SerializeField] private TMPro.TMP_Text _bombsLeftText;

        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private TMPro.TMP_Text _statusText;

        [SerializeField] private GameObject _playButton;

        public void ShowMenuUI() {

        }

        public void ShowGameUI() {

        }

        public void ShowGameOverUI(BoardStatus status) {
            string text = "";
            switch (status) {
                case BoardStatus.Lose:
                    text = "You LOSE!!!";
                    break;
                case BoardStatus.Win:
                    text = "You WIN!!!";
                    break;
            }
        }
    }
}
