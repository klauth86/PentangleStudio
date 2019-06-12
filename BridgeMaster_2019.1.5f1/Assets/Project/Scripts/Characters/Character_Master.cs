namespace BridgeMaster.Characters {
    public class Character_Master : Character_Base {
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
    }
}