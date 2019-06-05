using BridgeMaster.Dicts;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

namespace BridgeMaster.Masters {
    public class Master_Location : Base {
        [SerializeField] private Image _curtain;
        [SerializeField] private float _fadingDuration;
        [SerializeField] private int _fadingSteps;

        private void OnEnable() {
            Master.GameOverEvent += RestartLocation;
            Master.ExitLocationEvent += LoadLocation;
        }

        private void OnDisable() {
            Master.GameOverEvent -= RestartLocation;
            Master.ExitLocationEvent -= LoadLocation;
        }

        private void RestartLocation() {
            StartCoroutine(LoadLocationRoutine(SceneManager.GetActiveScene().buildIndex));
        }

        private void LoadLocation(Location location) {
            var sceneName = location.ToString();
            var scene = SceneManager.GetSceneByName(sceneName);
            StartCoroutine(LoadLocationRoutine(scene.buildIndex));
        }

        private IEnumerator LoadLocationRoutine(int buildIndex) {
            if (_curtain) {
                var delta = _fadingDuration / _fadingSteps;
                for (int i = 1; i <= _fadingSteps; i++) {
                    _curtain.color = new Color(_curtain.color.r, _curtain.color.g, _curtain.color.b, (1.0f * _fadingSteps - i) / _fadingSteps);
                    yield return new WaitForSeconds(delta);
                }
            }            
            SceneManager.LoadScene(buildIndex);
        }
    }
}