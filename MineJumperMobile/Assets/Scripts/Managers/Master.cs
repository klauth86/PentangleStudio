using UnityEngine;

namespace Managers {

    [RequireComponent(typeof(ManagerAudio))]
    [RequireComponent(typeof(ManagerBoard))]
    [RequireComponent(typeof(ManagerUI))]
    public class Master : MonoBehaviour {

        private ManagerAudio _audioManager;
        private ManagerBoard _boardManager;
        private ManagerUI _uiManager;

        // Use this for initialization
        void Start() {
            _audioManager = GetComponent<ManagerAudio>();
            _boardManager = GetComponent<ManagerBoard>();
            _uiManager = GetComponent<ManagerUI>();

            InitSubscribes();
        }

        private void InitSubscribes() {

            _uiManager.ButtonClicked += (b) => {
                _audioManager.PlayAudio(AudioClipType.ButtonClick);
                if (b == ManagerUI.ButtonRole.PlayButton)
                    _boardManager.CreateBoard(_uiManager.Size, _uiManager.Bombs);
            };

            _boardManager.GameStatusChanged += (s) => {
                switch (s) {
                    case Core.BoardStatus.Lose:
                        _audioManager.PlayAudio(AudioClipType.Lose);
                        break;
                    case Core.BoardStatus.Win:
                        _audioManager.PlayAudio(AudioClipType.Win);
                        break;
                }
                _uiManager.ShowGameOverUI(s);
            };
        }
    }

}