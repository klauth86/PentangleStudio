using System.Collections;
using UnityEngine;

namespace BridgeMaster.Masters {
    public class Master_ToggleMenu : Master_Base {
        [SerializeField] private GameObject _menu;
        [SerializeField] private string _toggleMenuButtonName;
        [SerializeField] private float _coroutineDeltaTime;

        private void OnEnable() {
            StartCoroutine(CheckForInputRequest());
        }

        private void OnDisable() {
            StopCoroutine(CheckForInputRequest());
        }

        private IEnumerator CheckForInputRequest() {
            while (true) {
                if (Input.GetButtonUp(_toggleMenuButtonName)) {
                    ToggleMenu();
                }
                yield return new WaitForSeconds(_coroutineDeltaTime);
            }
        }

        private void ToggleMenu() {
            _menu.SetActive(!_menu.activeSelf);
            Master.ToggleMenu();
        }
    }
}