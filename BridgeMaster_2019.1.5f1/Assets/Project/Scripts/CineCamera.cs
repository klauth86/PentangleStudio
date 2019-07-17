using BridgeMaster.Characters.Player;
using System.Collections;
using UnityEngine;

namespace BridgeMaster {
    [RequireComponent(typeof(Camera))]
    public class CineCamera : MonoBehaviour {
        [SerializeField] private float _cameraFollowTimeoutDuration;
        [SerializeField] private float _cameraFollowLerpDuration;

        [SerializeField] private float _vertOffsetInPercent;
        [SerializeField] private float _horOffsetInPercent;

        [SerializeField] private float _leftEdge;
        [SerializeField] private float _rightEdge;
        [SerializeField] private float _upEdge;
        [SerializeField] private float _downEdge;

        private Transform _target;
        public Transform Target {
            get { return _target ?? (_target = Player.Instance.transform); }
        }

        private Transform _transform;
        public Transform Transform {
            get { return _transform ?? (_transform = transform); }
        }

        private Camera _camera;
        public Camera Camera {
            get { return _camera ?? (_camera = GetComponent<Camera>()); }
        }

        private void OnEnable() {
            StartCoroutine(FollowRoutine());
        }

        private void OnDisable() {
            StopAllCoroutines();
        }

        IEnumerator FollowRoutine() {

            yield return new WaitUntil(() => Target != null);

            Transform.position = new Vector3(
                Mathf.Clamp(Target.position.x, _leftEdge, _rightEdge),
                Mathf.Clamp(Target.position.y, _downEdge, _upEdge), Transform.position.z);

            while (true) {
                var camx = Transform.position.x;
                var camy = Transform.position.y;

                if (Mathf.Abs(Target.position.x - camx) > _horOffsetInPercent * Camera.orthographicSize * Camera.aspect) {
                    camx = Target.position.x;
                }
                if (Mathf.Abs(Target.position.y - camy) > _vertOffsetInPercent * Camera.orthographicSize) {
                    camy = Target.position.y;
                }

                Transform.position = Vector3.Lerp(Transform.position, new Vector3(
                    _leftEdge < _rightEdge
                        ? Mathf.Clamp(camx, _leftEdge + Camera.orthographicSize * Camera.aspect, _rightEdge - Camera.orthographicSize * Camera.aspect)
                        : _leftEdge,
                    _downEdge < _upEdge
                        ? Mathf.Clamp(camy, _downEdge + Camera.orthographicSize, _upEdge - Camera.orthographicSize)
                        : _downEdge,
                    Transform.position.z), _cameraFollowLerpDuration);

                yield return new WaitForSeconds(_cameraFollowTimeoutDuration);
            }
        }        
    }
}