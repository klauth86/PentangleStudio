using BridgeMaster.Dicts;
using BridgeMaster.Game;

namespace BridgeMaster.Characters.Player {
    public class Player : Character_Master {

        private void OnEnable() {
            Game_Master.Instance.InputKeyEvent += OnInputKeyEvent;
        }

        private void OnDisable() {
            Game_Master.Instance.InputKeyEvent -= OnInputKeyEvent;
        }

        private void OnInputKeyEvent(InputAction action, InputActionState state, float axis) {
            switch (action) {
                case InputAction.RunAxisAction:
                    StartRun(axis);
                    break;
                case InputAction.JumpButton:
                    if (state == InputActionState.KeyDown)
                        StartJump();
                    break;
                case InputAction.CastSpellButton:
                    if (state == InputActionState.KeyDown)
                        StartCastSpell();
                    else
                        EndCastSpell();
                    break;
            }
        }

        #region SINGLE SESSION

        public static Player PlayerSession;

        private void Awake() {
            if (PlayerSession == null) {
                PlayerSession = this;
                DontDestroyOnLoad(gameObject);
            }
            else {
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }

        private Player() {}

        #endregion
    }
}
