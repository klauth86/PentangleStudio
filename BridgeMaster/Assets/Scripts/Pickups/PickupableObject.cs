using UnityEngine;

public class PickupableObject : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<Player>().AddPickup(this);
        Destroy(gameObject);
    }
}
