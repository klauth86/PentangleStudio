using BridgeMaster.Dicts;
using BridgeMaster.Engine;
using BridgeMaster.Game;

namespace BridgeMaster.Characters.Player {
    public class Player : CharacterMaster {

        protected override void CallOnEnableEvent() {
            GameMaster.Instance.InputKeyEvent += OnInputKeyEvent;
            base.CallOnEnableEvent();
        }

        protected override void CallOnDisableEvent() {
            GameMaster.Instance.InputKeyEvent -= OnInputKeyEvent;
            base.CallOnDisableEvent();
        }

        private void OnInputKeyEvent(InputActions action, InputActionStates state, float axis) {
            switch (action) {
                case InputActions.RunAxisAction:
                    StartRun(axis);
                    break;
                case InputActions.JumpButton:
                    if (state == InputActionStates.KeyDown)
                        StartJump();
                    break;
                case InputActions.CastSpellButton:
                    if (state == InputActionStates.KeyDown)
                        StartCastSpell();
                    else
                        EndCastSpell();
                    break;
            }
        }

        #region SINGLETON

        public static Player Instance;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else {
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }

        private Player():base(CharacterStateFactory.GetPlayerState()) {}

        #endregion
    }
}
