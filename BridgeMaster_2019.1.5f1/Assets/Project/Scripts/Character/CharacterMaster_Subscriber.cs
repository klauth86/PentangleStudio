using BridgeMaster.Base;

namespace BridgeMaster.Characters {
    public abstract class CharacterMaster_Subscriber<T> : COM_Subscriber<T> where T : CharacterMaster {

        #region EVENTS

        protected override void Subscribe() {
            if (!_isSubscribed) {
                Master.IsDeadEvent += IsDead;
                base.Subscribe();
            }
        }

        protected override void Unsubscribe() {
            if (_isSubscribed) {
                Master.IsDeadEvent -= IsDead;
                base.Unsubscribe();
            }
        }

        protected virtual void IsDead() {
            Destroy(this);
        }

        #endregion
    }
}