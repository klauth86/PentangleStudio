using BridgeMaster.Engine;
using UnityEngine;

namespace BridgeMaster.Characters.AI {
    public class AIMaster : CharacterMaster {

        [SerializeField] private bool _isChasing;

        #region CTOR

        public AIMaster() : base(CharacterStateFactory.GetPlayerState()) { }

        #endregion

        private void Update() {

            if (!IsReadyForAttack) {
                if (_isChasing) {
                    var direction = Target
                        ? MathFacade.Sign(Target.Transform.position.x - Transform.position.x)
                        : 0.0f; // TODO PATROLING

                    SetRun(direction);
                }
            }
        }        

        private CharacterMaster _target;
        public CharacterMaster Target {
            get { return _target; }
            set {
                if (value == _target)
                    return;

                if (_target != null)
                    UnsubscribeFromTarget(_target);

                _target = value;

                if (_target != null)
                    SubscribeToTarget(_target);
            }
        }

        private void SubscribeToTarget(CharacterMaster target) {
            target.CharacterState.DieEvent += Die;
        }

        private void UnsubscribeFromTarget(CharacterMaster target) {
            target.CharacterState.DieEvent -= Die;
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

                if (_isReadyForAttack) {
                    SetRun(0);
                    StartAttack();
                }
                else
                    EndAttack();
            }
        }

        public void Attack() {

        }
    }
}
