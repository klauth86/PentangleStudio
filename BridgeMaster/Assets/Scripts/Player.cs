using UnityEngine;

public class Player : MonoBehaviour {
    // Configuration
    [SerializeField] private float _velocity;
    [SerializeField] private float _jump;
    [SerializeField] private float _health;

    // Cache
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private Animator _animator;

    [SerializeField] private Spell _selectedSpell;

    private bool _isJumping;

    // Update is called once per frame
    void Update() {
        Run();
        Jump();
        Cast();
        Land();
    }

    private void Land() {
        if (Mathf.Abs(_rigidbody.velocity.y) < float.Epsilon) {
            _isJumping = false;
        }
    }

    private void Run() {
        var horAxis = Input.GetAxis("Horizontal");
        var horVelocity = _velocity * horAxis;
        _rigidbody.velocity = new Vector2(_velocity * horAxis, _rigidbody.velocity.y);
        _animator.SetBool("IsRunning", Mathf.Abs(horVelocity) > 0);
        transform.localScale = new Vector3(Mathf.Sign(horVelocity), 1, 1);
    }

    private void Jump() {
        if (Input.GetButtonDown("Fire1")) {
            if (!_isJumping) {
                _isJumping = true;
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jump);
            }
        }
    }

    protected Coroutine _coroutine;

    protected void Cast() {
        if (Input.GetButtonDown("Fire2") && _selectedSpell) {
            Debug.Log("Fire2 GetButtonDown");
            _selectedSpell.StartCast(transform.position + new Vector3(0.1f, 0.1f, 0),
                transform.position + new Vector3(10, 10, 0));
        }
        if (Input.GetButtonUp("Fire2") && _selectedSpell) {
            Debug.Log("Fire2 GetButtonUp");
            _selectedSpell.StopCast();
        }
    }
}