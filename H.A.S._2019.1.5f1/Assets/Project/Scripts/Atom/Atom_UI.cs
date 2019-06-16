using UnityEngine;

namespace HAS.Atom {
    public class Atom_UI : Atom_SubBase {
        [SerializeField] TMPro.TMP_Text _N_TextMP;
        [SerializeField] TMPro.TMP_Text _L_TextMP;
        [SerializeField] TMPro.TMP_Text _K_TextMP;
        [SerializeField] TMPro.TMP_Text _lambda_TextMP;

        private void OnEnable() {
            Master.ChangeNEvent += OnChangeNEvent;
            Master.ChangeLEvent += OnChangeLEvent;
            Master.ChangeKEvent += OnChangeKEvent;
            Master.ChangeLambdaEvent += OnChangeLambdaEvent;
        }

        private void OnDisable() {
            Master.ChangeNEvent -= OnChangeNEvent;
            Master.ChangeLEvent -= OnChangeLEvent;
            Master.ChangeKEvent -= OnChangeKEvent;
            Master.ChangeLambdaEvent -= OnChangeLambdaEvent;
        }

        private void OnChangeNEvent(int oldValue, int newValue) {
            if (_N_TextMP) {
                _N_TextMP.text = newValue.ToString();
            }
            else {
                Debug.LogWarning("N_TextMP is not set in the inspector!");
            }
        }

        private void OnChangeLEvent(int oldValue, int newValue) {
            if (_L_TextMP) {
                _L_TextMP.text = newValue.ToString();
            }
            else {
                Debug.LogWarning("L_TextMP is not set in the inspector!");
            }
        }

        private void OnChangeKEvent(int oldValue, int newValue) {
            if (_K_TextMP) {
                _K_TextMP.text = newValue.ToString();
            }
            else {
                Debug.LogWarning("K_TextMP is not set in the inspector!");
            }
        }

        private void OnChangeLambdaEvent(double lambda) {
            if (_lambda_TextMP) {
                _lambda_TextMP.text = lambda.ToString();
                _lambda_TextMP.color = ColorConverter.Global.ConvertWavelengthToColor(lambda); 
            }
            else {
                Debug.LogWarning("_lambda_TextMP is not set in the inspector!");
            }
        }
    }
}
