using UnityEngine;
namespace Managers {
    public class Master : MonoBehaviour {

        private ManagerAudio _audioManager;
        private ManagerBoard _boardManager;
        private ManagerUI _uiManager;

        // Use this for initialization
        void Start() {
            _audioManager = GetComponent<ManagerAudio>();
            _boardManager = GetComponent<ManagerBoard>();
            _uiManager = GetComponent<ManagerUI>();
        }
    }
}