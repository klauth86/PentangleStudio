using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour {
    [SerializeField] private float _velocity;
    [SerializeField] private float _jump;
    [SerializeField] private float _health;

    private bool _isJumping;
    private Rigidbody2D _rigidbody;

    // Use this for initialization
    void Start() {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        Run();
        Jump();
        Land();
    }

    void Run() {
        var horAxis = Input.GetAxis("Horizontal");
        var horVelocity = _velocity * horAxis;
        _rigidbody.velocity = new Vector2(_velocity * horAxis, _rigidbody.velocity.y);
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
        }
    }    

    void Land() {
        _isJumping = Mathf.Abs(_rigidbody.velocity.y) < float.Epsilon;
    }
}
