using Base;
using Dicts;
using UnityEngine;

public class Player : CharacterWithPhysics {
    [SerializeField] private float _velocity;
    [SerializeField] private float _jump;
    [SerializeField] public float HitPoints;

    [SerializeField] private GameObject _damageVfx;

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
        var horAxis = Input.GetAxis(InputAxis.Horizontal);
        var horVelocity = _velocity * horAxis;
        _rigidbody.velocity = new Vector2(_velocity * horAxis, _rigidbody.velocity.y);
        _animator.SetBool(AnimatorKey.IsWalking, horAxis != 0);

        Swap(horVelocity);
    }

    void Swap(float horVelocity) {
        if (Mathf.Abs(horVelocity) > float.Epsilon)
            transform.localScale = new Vector3(Mathf.Sign(horVelocity), 1, 1);
    }

    #endregion

    #region Jump

    void Jump() {
        if (Input.GetButtonDown(Button.Fire1) && !_isJumping) {
            _isJumping = true;
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jump);
            _animator.SetBool(AnimatorKey.IsJumping, true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (_isJumping) {
            _isJumping = false;
            _animator.SetBool(AnimatorKey.IsJumping, false);
        }
    }

    #endregion

    #region Attack

    void Attack() {
        if (Input.GetButtonDown(Button.Fire2)) {
            _animator.SetBool(AnimatorKey.IsAttacking, true);
        }
        else if (Input.GetButtonUp(Button.Fire2)) {
            _animator.SetBool(AnimatorKey.IsAttacking, false);
        }
    }

    #endregion

    public bool TakeDamage(Damage damage) {
        HitPoints -= damage.Hit;
        if (_damageVfx) {
            Destroy(Instantiate(_damageVfx), 2f);
        }
        if (HitPoints < 0) {
            OnUpdate -= Walk;
            OnUpdate -= Jump;
            OnUpdate -= Attack;
            _rigidbody.velocity = Vector3.zero;
            _animator.SetTrigger(AnimatorKey.IsDead);
        }
        return HitPoints > 0;
    }
}
