using System;
using UnityEngine;

public class Card : MonoBehaviour, ICard {
    [SerializeField] private Animator _animator;

    [SerializeField] private float _rotationRate;
    [SerializeField] private Vector3 _rotationVector;

    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Material[] _materials;

    public bool IsMarked { get; set; }
    public bool IsTrump { get; set; }
    public int Index { get; set; }

    public void Flip() {
        if (IsTrump && _animator)
            _animator.SetTrigger("Explosion");
    }

    private void Start() {
        SetMaterial(Index);
        _rotationVector = new Vector3(
            UnityEngine.Random.Range(0.0f, 1.0f),
            UnityEngine.Random.Range(0.0f, 1.0f),
            UnityEngine.Random.Range(0.0f, 1.0f)).normalized;
    }

    private void SetMaterial(int index) {
        _meshRenderer.material = _materials[index];
    }

    private void Update() {
        transform.Rotate(_rotationVector * Time.deltaTime * _rotationRate);
    }
}
