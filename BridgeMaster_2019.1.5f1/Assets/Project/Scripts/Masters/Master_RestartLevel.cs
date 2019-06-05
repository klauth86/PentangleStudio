using UnityEngine.SceneManagement;

namespace BridgeMaster.Masters {
    public class Master_RestartLevel : Base {

        private void OnEnable() {
            Master.GameOverEvent += RestartLevel;
        }

        private void OnDisable() {
            Master.GameOverEvent -= RestartLevel;
        }

        private void RestartLevel() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}