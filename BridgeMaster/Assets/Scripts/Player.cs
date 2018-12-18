using System.Collections;
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
    [SerializeField] private float _castReTime = 0.8f;

    private bool _isJumping;

    // Use this for initialization
    void Start() {
    }

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
        if (Input.GetButtonDown("Fire2")) {
            _coroutine = StartCoroutine(CastCoroutine());
        }
        if (Input.GetButtonUp("Fire2") && _coroutine != null) {
            StopCoroutine(_coroutine);
        }
    }

    protected IEnumerator CastCoroutine() {
        //    var start = true;
        //    var vector = new Vector2(185/512, 0);
        //    while (true) {
        //        GameSingleton.Singleton.TryCast(_selectedSpell, start, vector);
        //        vector += new Vector2(212/512, 0);
        //        yield return new WaitForSeconds(_castReTime);
        //    }
        return null;
    }
}