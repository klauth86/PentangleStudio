using Base;
using UnityEngine;

public class Player : PhysicalCharacter {
    [SerializeField] private float _velocity;
    [SerializeField] private float _jump;
    [SerializeField] private float _health;

    private bool _isJumping;

    // Update is called once per frame
    void Update() {
        Walk();
        Jump();
        Attack();
    }

    void Walk() {
        var horAxis = Input.GetAxis("Horizontal");
        var horVelocity = _velocity * horAxis;
        _rigidbody.velocity = new Vector2(_velocity * horAxis, _rigidbody.velocity.y);
        _animator.SetBool("IsWalking", horAxis != 0);

        Swap(horVelocity);
    }

    void Swap(float horVelocity) {
        if (Mathf.Abs(horVelocity) > float.Epsilon)
            transform.localScale = new Vector3(Mathf.Sign(horVelocity), 1, 1);
    }

    void Jump() {
        if (Input.GetButtonDown("Fire1") && !_isJumping) {
            _isJumping = true;
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jump);
            _animator.SetBool("IsJumping", true);
        }
    }

    void Attack() {
        if (Input.GetButtonDown("Fire2")) {
            _animator.SetBool("IsAttacking", true);
        }
        else if (Input.GetButtonUp("Fire2")) {
            _animator.SetBool("IsAttacking", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (_isJumping) {
            _isJumping = false;
            _animator.SetBool("IsJumping", false);
        }
    }
}
