using System.Collections;
using UnityEngine;

namespace HAS.Atom {
    public class Atom_KeyInput : Atom_SubBase {
        [SerializeField] private string _buttonName;
        [SerializeField] private float _processRate;

        private void OnEnable() {
            StartCoroutine(ProcessKeyInputRoutine());
        }

        private void OnDisable() {
            StopAllCoroutines();
        }

        private IEnumerator ProcessKeyInputRoutine() {
            yield return new WaitForSeconds(_processRate);
            bool isGrowing = false; ;
            double currentLambda;
            UpdateLambda(out currentLambda, ColorConverter.Global.minLambda);

            while (true) {
                if (Input.GetButtonDown(_buttonName)) {
                    isGrowing = true; ;
                }
                if (Input.GetButtonUp(_buttonName)) {
                    isGrowing = false;
                    Master.CallDevourPhotonEvent(Constants.c * 2E9 * Mathf.PI / currentLambda);
                    UpdateLambda(out currentLambda, ColorConverter.Global.minLambda);
                }

                if (isGrowing && currentLambda < ColorConverter.Global.maxLambda + 1) {
                    UpdateLambda(out currentLambda, currentLambda + 1);
                }

                yield return new WaitForSeconds(_processRate);
            }
        }

        private void UpdateLambda(out double currentLambda, double value) {
            currentLambda = value;
            Master.CallChangeLambdaEvent(currentLambda);
        }
    }
}