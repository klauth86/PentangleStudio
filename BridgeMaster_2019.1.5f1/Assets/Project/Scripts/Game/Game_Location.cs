using BridgeMaster.Dicts;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

namespace BridgeMaster.Game {
    public class Game_Location : Game_Base {
        [SerializeField] private Image _curtain;
        [SerializeField] private float _fadingDuration;
        [SerializeField] private int _fadingSteps;

        private void OnEnable() {
            Master.GameOverEvent += RestartLocation;
            Master.EnterLocationEvent += StartLocation;
            Master.ExitLocationEvent += LoadLocation;
        }

        private void OnDisable() {
            Master.GameOverEvent -= RestartLocation;
            Master.EnterLocationEvent -= StartLocation;
            Master.ExitLocationEvent -= LoadLocation;
        }

        private void RestartLocation() {
            StartCoroutine(LoadLocationRoutine(SceneManager.GetActiveScene().buildIndex));
        }

        private void StartLocation() {
            StartCoroutine(StartLocationRoutine());
        }

        private IEnumerator StartLocationRoutine() {
            if (_curtain) {
                var delta = _fadingDuration / _fadingSteps;
                for (int i = 1; i <= _fadingSteps; i++) {
                    _curtain.color = new Color(_curtain.color.r, _curtain.color.g, _curtain.color.b, (1.0f * _fadingSteps - i) / _fadingSteps);
                    yield return new WaitForSeconds(delta);
                }
            }
        }

        private void LoadLocation(Location location) {
            StartCoroutine(LoadLocationRoutine((int)location));
        }

        private IEnumerator LoadLocationRoutine(int buildIndex) {
            if (_curtain) {
                var delta = _fadingDuration / _fadingSteps;
                for (int i = 1; i <= _fadingSteps; i++) {
                    _curtain.color = new Color(_curtain.color.r, _curtain.color.g, _curtain.color.b, (1.0f * i) / _fadingSteps);
                    yield return new WaitForSeconds(delta);
                }
            }            
            SceneManager.LoadScene(buildIndex);
        }
    }
}