using BridgeMaster.Dicts;
using UnityEngine;

namespace BridgeMaster.Characters {
    class Character_Run : Base<Master> {
        [SerializeField] private float _velocity;

        private void OnEnable() {
            Master.StartRunEvent += StartRun;
            Master.EndRunEvent += EndRun;
        }

        private void OnDisable() {
            Master.StartRunEvent -= StartRun;
            Master.EndRunEvent -= EndRun;
        }

        private void StartRun(float axis) {
            Master.Rigidbody.velocity = new Vector2(_velocity * axis, Master.Rigidbody.velocity.y);
            Master.Animator.SetBool(AnimatorKey.IsRunning, axis != 0);
            Swap(axis);
        }

        private void EndRun() {
            Master.Animator.SetBool(AnimatorKey.IsRunning, false);
        }

        void Swap(float axis) {
            if (Mathf.Abs(axis) > float.Epsilon)
                transform.localScale = new Vector3(Mathf.Sign(axis), 1, 1);
        }
    }
}