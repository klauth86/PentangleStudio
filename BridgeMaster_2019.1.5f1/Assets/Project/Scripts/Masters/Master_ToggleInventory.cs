using System.Collections;
using UnityEngine;

namespace BridgeMaster.Masters {
    public class Master_ToggleInventory : Master_Base {
        [SerializeField] private GameObject _inventory;
        [SerializeField] private string _toggleInventoryButtonName;
        [SerializeField] private float _coroutineDeltaTime;

        private void OnEnable() {
            StartCoroutine(CheckForInputRequest());
        }

        private void OnDisable() {
            StopCoroutine(CheckForInputRequest());
        }

        private IEnumerator CheckForInputRequest() {
            while (true) {
                if (Input.GetButtonUp(_toggleInventoryButtonName)) {
                    ToggleInventory();
                }
                yield return new WaitForSeconds(_coroutineDeltaTime);
            }
        }

        private void ToggleInventory() {
            _inventory.SetActive(!_inventory.activeSelf);
            Master.ToggleInventory();
        }
    }
}