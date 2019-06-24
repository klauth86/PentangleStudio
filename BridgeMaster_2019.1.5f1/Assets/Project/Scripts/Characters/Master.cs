using BridgeMaster.Dicts;

namespace BridgeMaster.Characters {
    public class Master : GameObjectSubscriber<Game.Master> {
        public event GameEventHandler DieEvent;

        public event GameEventHandler<float> ChangeHealthEvent;
        public event GameEventHandler<float> ChangeManaEvent;
        public event GameEventHandler<float> ChangeIronEvent;

        public event GameEventHandler<float> StartRunEvent;
        public event GameEventHandler EndRunEvent;

        public event GameEventHandler StartJumpEvent;
        public event GameEventHandler EndJumpEvent;

        public event GameEventHandler StartAttackEvent;
        public event GameEventHandler EndAttackEvent;

        public event GameEventHandler StartCastSpellEvent;
        public event GameEventHandler EndCastSpellEvent;

        public event GameEventHandler FreezeEvent;
        public event GameEventHandler UnfreezeEvent;

        public void Die() {
            DieEvent?.Invoke();
            enabled = false;
        }

        public void ChangeHealth(float value) {
            ChangeHealthEvent?.Invoke(value);
        }

        public void ChangeMana(float value) {
            ChangeManaEvent?.Invoke(value);
        }

        public void ChangeIron(float value) {
            ChangeIronEvent?.Invoke(value);
        }

        public void StartRun(float axis) {
            StartRunEvent?.Invoke(axis);
        }

        public void EndRun() {
            EndRunEvent?.Invoke();
        }

        public void StartJump() {
            StartJumpEvent?.Invoke();
        }

        public void EndJump() {
            EndJumpEvent?.Invoke();
        }

        public void StartAttack() {
            StartAttackEvent?.Invoke();
        }

        public void EndAttack() {
            EndAttackEvent?.Invoke();
        }

        public void StartCastSpell() {
            StartCastSpellEvent?.Invoke();
        }

        public void EndCastSpell() {
            EndCastSpellEvent?.Invoke();
        }

        public void Freeze() {
            FreezeEvent?.Invoke();
        }

        public void Unfreeze() {
            UnfreezeEvent?.Invoke();
        }

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