using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private float _minY;
    [SerializeField] private float _deltaY;

    private Rigidbody _rigidbody;
    private Rigidbody Rigidbody {
        get {
            return _rigidbody ?? (_rigidbody = GetComponent<Rigidbody>());
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (transform.position.y < _minY + _deltaY)
            Rigidbody.velocity =  - Rigidbody.velocity;

    }
}
