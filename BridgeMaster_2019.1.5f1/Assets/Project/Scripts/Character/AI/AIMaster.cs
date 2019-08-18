using BridgeMaster.Engine;
using UnityEngine;

namespace BridgeMaster.Characters.AI {
    public abstract class AIMaster : CharacterMaster {

        [SerializeField] protected bool _isChasing;
        [SerializeField] protected float _attackRadius = 7;

        [SerializeField] protected Transform[] _checkPoints;
        [SerializeField] protected float _checkPointRadius = 0.25f;

        private int _lastCheckPointIndex;

        #region CTOR

        public AIMaster(CharacterState state) : base(state) { }

        #endregion

        private void Update() {

            if (!IsReadyForAttack) {
                if (_isChasing && Target) {
                    SetRun(MathFacade.Sign(Target.Transform.position.x - Transform.position.x));
                }
                else {
                    if (_checkPoints != null && _checkPoints.Length > 0) {
                        if (MathFacade.Abs(_checkPoints[_lastCheckPointIndex].position.x - Transform.position.x) < _checkPointRadius)
                            _lastCheckPointIndex = (_lastCheckPointIndex + 1) % _checkPoints.Length;

                        SetRun(MathFacade.Sign(_checkPoints[_lastCheckPointIndex].position.x - Transform.position.x));
                    }
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
            target.CharacterState.DieEvent += TargetDies;
        }

        private void UnsubscribeFromTarget(CharacterMaster target) {
            target.CharacterState.DieEvent -= TargetDies;
        }

        private void TargetDies() {
            IsReadyForAttack = false;
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



        protected override void Attack() {
            if (Target) {
                if ((Target.Transform.position.x - Transform.position.x) * Transform.right.x > 0 &&
                    Target.Transform.position.x - Transform.position.x < _attackRadius)
                    Target.CharacterState.ChangeHealth(-CharacterState.GetAttack());
            }
        }

        protected override void Die() {}

        #region GIZMO
#if UNITY_EDITOR
        private void OnDrawGizmos() {
            Gizmos.DrawLine(Transform.position, Transform.position + Transform.right * _attackRadius);

            if (_checkPoints != null)
                foreach (var item in _checkPoints) {
                    if (item)
                        Gizmos.DrawWireSphere(item.position, _checkPointRadius);
                }
        }
#endif
        #endregion
    }
}
