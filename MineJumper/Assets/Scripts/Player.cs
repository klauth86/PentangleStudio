using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour {
    private Rigidbody _rigidbody;
    private Rigidbody Rigidbody {
        get {
            return _rigidbody ?? (_rigidbody = GetComponent<Rigidbody>());
        }
    }

    private float _velocity;

    public void Freeze() {
        _velocity = Rigidbody.velocity.y;
        Rigidbody.constraints |= RigidbodyConstraints.FreezePositionY;
    }

    public void Unfreeze() {
        Rigidbody.constraints &= ~RigidbodyConstraints.FreezePositionY;
        Rigidbody.velocity = new Vector3(Rigidbody.velocity.x, _velocity, Rigidbody.velocity.z);
    }

}
