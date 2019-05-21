using Base;
using UnityEngine;

public class Player : CharacterWithPhysics {
    [SerializeField] private float _velocity;
    [SerializeField] private float _jump;
    [SerializeField] private float _health;

    private bool _isJumping;

    #region Ctor

    public Player() {
        OnUpdate += Walk;
        OnUpdate += Jump;
        OnUpdate += Attack;
    }

    #endregion

    #region Walk

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

    #endregion

    #region Jump

    void Jump() {
        if (Input.GetButtonDown("Fire1") && !_isJumping) {
            _isJumping = true;
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jump);
            _animator.SetBool("IsJumping", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (_isJumping) {
            _isJumping = false;
            _animator.SetBool("IsJumping", false);
        }
    }

    #endregion

    #region Attack

    void Attack() {
        if (Input.GetButtonDown("Fire2")) {
            _animator.SetBool("IsAttacking", true);
        }
        else if (Input.GetButtonUp("Fire2")) {
            _animator.SetBool("IsAttacking", false);
        }
    }

    #endregion
}
