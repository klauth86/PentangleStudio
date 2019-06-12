using System.Collections;
using UnityEngine;

namespace BridgeMaster {
    [RequireComponent(typeof(Camera))]
    public class CineCamera : MonoBehaviour {
        [SerializeField] private Transform _target;
        [SerializeField] private float _cameraFollowTimeoutDuration;
        [SerializeField] private float _cameraFollowLerpDuration;

        [SerializeField] private float _vertOffsetInPercent;
        [SerializeField] private float _horOffsetInPercent;

        [SerializeField] private float _leftEdge;
        [SerializeField] private float _rightEdge;
        [SerializeField] private float _upEdge;
        [SerializeField] private float _downEdge;

        private Transform _transform;
        private Camera _camera;

        private void Awake() {
            if (_target) {
                transform.position = new Vector3(
                    Mathf.Clamp(_target.position.x, _leftEdge, _rightEdge),
                    Mathf.Clamp(_target.position.y, _downEdge, _upEdge), transform.position.z);
            }
            else {
                Debug.LogWarning("_transform is not init in Inspector!");
            }
            _transform = transform;
            _camera = GetComponent<Camera>();
        }

        private void OnEnable() {
            StartCoroutine(FollowRoutine());
        }

        private void OnDisable() {
            StopAllCoroutines();
        }

        IEnumerator FollowRoutine() {
            while (_target) {
                var camx = _transform.position.x;
                var camy = _transform.position.y;

                if (Mathf.Abs(_target.position.x - camx) > _horOffsetInPercent * _camera.orthographicSize * _camera.aspect) {
                    camx = _target.position.x;
                }
                if (Mathf.Abs(_target.position.y - camy) > _vertOffsetInPercent * _camera.orthographicSize) {
                    camy = _target.position.y;
                }
                _transform.position = Vector3.Lerp(_transform.position, new Vector3(
                    _leftEdge < _rightEdge
                        ? Mathf.Clamp(camx, _leftEdge + _camera.orthographicSize * _camera.aspect, _rightEdge - _camera.orthographicSize * _camera.aspect)
                        : _leftEdge,
                    _downEdge < _upEdge
                        ? Mathf.Clamp(camy, _downEdge + _camera.orthographicSize, _upEdge - _camera.orthographicSize)
                        : _downEdge,
                    _transform.position.z), _cameraFollowLerpDuration);
                yield return new WaitForSeconds(_cameraFollowTimeoutDuration);
            }
        }        
    }
}