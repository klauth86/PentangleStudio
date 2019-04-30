using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Mob01 : MonoBehaviour {
    [SerializeField] private float _velocity;
    [SerializeField] private float _health;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private Collider2D _collider;

    // Use this for initialization
    void Start () {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _collider = GetComponentInChildren<Collider2D>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
