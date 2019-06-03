using UnityEngine;
namespace Managers {
    public class MasterManager : MonoBehaviour {

        private AudioManager _audioManager;
        private BoardManager _boardManager;
        private UIManager _uiManager;

        // Use this for initialization
        void Start() {
            _audioManager = GetComponent<AudioManager>();
            _boardManager = GetComponent<BoardManager>();
            _uiManager = GetComponent<UIManager>();
        }
    }
}