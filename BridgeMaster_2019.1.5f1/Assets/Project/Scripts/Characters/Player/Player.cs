using BridgeMaster.Dicts;

namespace BridgeMaster.Characters.Player {
    public class Player:Master {
        private void OnEnable() {
            Target.InputKeyEvent += OnInputKeyEvent;
        }

        private void OnDisable() {
            Target.InputKeyEvent -= OnInputKeyEvent;
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
    }
}
