using Base;
using UnityEngine;

public class Mob01 : AnimatedCharacter {
    [Header("Movement")]
    [SerializeField] private bool isMoving;
    [SerializeField] private Transform leftPoint;
    [SerializeField] private Transform rightPoint;
    
    [Header("Stats")]
    [SerializeField] private float _velocity;
    [SerializeField] private float _health;

    [Header("Behaviour")]
    [SerializeField] private Collider2D _attackRadius;
    [SerializeField] private Collider2D _tryAttackRadius;
    [SerializeField] private Collider2D _huntRadius;

    private Transform _nextPoint;

    protected override void Init() {
        base.Init();
        if (leftPoint && rightPoint) 
            DefineNextPoint();        
    }

    private void DefineNextPoint() {
        var leftDistance = Vector3.Distance(leftPoint.position, transform.position);
        var rightDistance = Vector3.Distance(rightPoint.position, transform.position);
        if (leftDistance > rightDistance)
            _nextPoint = leftPoint;
        else
            _nextPoint = rightPoint;
    }

    // Update is called once per frame
    void Update () {
        Walk();
        Attack();
	}

    private void Attack() {
    }

    private void Walk() {
        if (isMoving) {
            var direction = Mathf.Sign(_nextPoint.position.x - transform.position.x);
            transform.position = new Vector3(transform.position.x + _velocity * direction * Time.deltaTime, transform.position.y, transform.position.z);
            _animator.SetBool("IsWalking", isMoving);

            if (Mathf.Sign(_nextPoint.position.x - transform.position.x)!= direction) {
                DefineNextPoint();
                Swap(-direction);
            }
        }
    }

    void Swap(float horVelocity) {
        if (Mathf.Abs(horVelocity) > float.Epsilon)
            transform.localScale = new Vector3(Mathf.Sign(horVelocity), 1, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.GetComponent<Player>()) {
            _nextPoint = collision.gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.GetComponent<Player>() && isMoving) {
            DefineNextPoint();
        }
    }
}
