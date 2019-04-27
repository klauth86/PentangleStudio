﻿using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour {
    [SerializeField] private float _velocity;
    [SerializeField] private float _jump;
    [SerializeField] private float _health;

    private bool _isJumping;
    private Rigidbody2D _rigidbody;
    private Animator _animator;

    // Use this for initialization
    void Start() {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        Run();
        Jump();
        CastSpell();
    }

    void Run() {
        var horAxis = Input.GetAxis("Horizontal");
        var horVelocity = _velocity * horAxis;
        _rigidbody.velocity = new Vector2(_velocity * horAxis, _rigidbody.velocity.y);
        Swap(horVelocity);
        _animator.SetBool("IsRunning", horAxis != 0);
    }

    void Swap(float horVelocity) {
        if (Mathf.Abs(horVelocity) > float.Epsilon)
            transform.localScale = new Vector3(Mathf.Sign(horVelocity), 1, 1);
    }

    void Jump() {
        if (Input.GetButtonDown("Fire1")) {
            if (!_isJumping) {
                _isJumping = true;
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jump);
                _animator.SetBool("IsJumping", true);
            }
        }
        if (_isJumping && Mathf.Abs(_rigidbody.velocity.y) < float.Epsilon) {
            _isJumping = false;
            _animator.SetBool("IsJumping", false);
        }
    }

    void CastSpell() {
    }
}
