using UnityEngine;

public class CineCamera : MonoBehaviour
{
    [SerializeField] private Transform _target;

    [SerializeField] private float _vertOffset;
    [SerializeField] private float _horOffset;

    [SerializeField] private float _leftEdge;
    [SerializeField] private float _upEdge;
    [SerializeField] private float _rightEdge;
    [SerializeField] private float _downEdge;

    private Transform _transform;

    private void Start() {
        if (_target) {
            transform.position = _target.position;
        }
        else {
            Debug.LogWarning("_transform is not init in Inspector!");
        }
        _transform = transform;
    }

    private void FixedUpdate() {
    }
}
