using BridgeMaster.Dicts;
using BridgeMaster.Engine;
using BridgeMaster.Game;

namespace BridgeMaster.Characters.Player {
    public class PlayerMaster : CharacterMaster {

        #region SINGLETON

        public static PlayerMaster Instance;

        protected override void CallOnAwakeEvent() {

            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else {
                gameObject.SetActive(false);
                Destroy(gameObject);
            }

            base.CallOnAwakeEvent();
        }

        private PlayerMaster() : base(CharacterStateFactory.GetPlayerState()) { }

        #endregion

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
                    SetRun(axis);
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



        protected override void Attack() {
        }
    }
}
