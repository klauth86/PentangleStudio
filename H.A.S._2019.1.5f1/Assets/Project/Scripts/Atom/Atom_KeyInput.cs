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
            bool isGrowing = false; ;
            double currentLambda = ColorConverter.Global.minLambda;
            Master.CallChangeLambdaEvent(currentLambda);

            while (true) {
                if (Input.GetButtonDown(_buttonName)) {
                    isGrowing = true; ;
                }

                if (Input.GetButtonUp(_buttonName)) {
                    Master.CallDevourPhotonEvent(Constants.c*2*Mathf.PI/ currentLambda);
                    isGrowing = false;
                    currentLambda = ColorConverter.Global.minLambda;
                    Master.CallChangeLambdaEvent(currentLambda);
                }

                if (isGrowing && currentLambda < ColorConverter.Global.maxLambda+1) {
                    currentLambda += 1;
                    Master.CallChangeLambdaEvent(currentLambda);
                }                  

                yield return new WaitForSeconds(_processRate);
            }
        }
    }
}
