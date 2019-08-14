using BridgeMaster.Engine;
using UnityEngine;

namespace BridgeMaster.Characters.AI {
    public class AIMaster : CharacterMaster {

        [SerializeField] private bool _isChasing;

        private CharacterMaster _target;
        public CharacterMaster Target {
            get { return _target; }
            set {
                if (value == _target)
                    return;

                if (_target != null)
                    Unsubscribe(_target);

                _target = value;

                if (_target != null)
                    Subscribe(_target);
            }
        }

        private void Unsubscribe(CharacterMaster target) {
            target.CharacterState.DieEvent -= Die;
        }

        private void Subscribe(CharacterMaster target) {
            target.CharacterState.DieEvent += Die;
        }

        private void Die() {
            EndAttack();
        }



        private bool _isReadyForAttack;
        public bool IsReadyForAttack {
            get { return _isReadyForAttack; }
            set {
                if (value == _isReadyForAttack)
                    return;

                _isReadyForAttack = value;

                if (_isReadyForAttack)
                    StartAttack();
                else
                    EndAttack();
            }
        }

        private void Update() {
            var direction = Target
                ? MathFacade.Sign(Target.Transform.position.x - Transform.position.x)
                : 0.0f;

            if (!IsReadyForAttack && _isChasing) {
                if (direction != 0.0f)
                    StartRun(direction);
                else
                    EndRun();
            }
        }

        public void Attack() {
            
        }

        #region CTOR

        public AIMaster() : base(CharacterStateFactory.GetPlayerState()) { }

        #endregion
    }
}
