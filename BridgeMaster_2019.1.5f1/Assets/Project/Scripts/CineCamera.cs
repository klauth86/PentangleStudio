using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CineCamera : MonoBehaviour {
    [SerializeField] private Transform _target;
    [SerializeField] private float _cameraFollowDuration;

    [SerializeField] private float _vertOffsetInPercent;
    [SerializeField] private float _horOffsetInPercent;

    [SerializeField] private float _leftEdge;
    [SerializeField] private float _upEdge;
    [SerializeField] private float _rightEdge;
    [SerializeField] private float _downEdge;

    private Transform _transform;
    private Camera _camera;

    private void Awake() {
        if (_target) {
            transform.position = new Vector3(_target.position.x, _target.position.y, transform.position.z);
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
        while (true) {
            var camx = _transform.position.x;
            var camy = _transform.position.y;

            if (Mathf.Abs(_target.position.x - camx) > _horOffsetInPercent * _camera.orthographicSize * _camera.aspect) {
                camx = _target.position.x;
            }
            if (Mathf.Abs(_target.position.y - camy) > _vertOffsetInPercent * _camera.orthographicSize) {
                camy = _target.position.y;
            }
            _transform.position = Vector3.Lerp(_transform.position, new Vector3(camx, camy, _transform.position.z), _cameraFollowDuration);
            yield return new WaitForSeconds(_cameraFollowDuration);
        }
    }
}