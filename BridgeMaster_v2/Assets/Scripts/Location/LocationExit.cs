using Dicts;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LocationExit : MonoBehaviour {
    [SerializeField] private Location _nextLocation;
    [SerializeField] private LocationExitVfx _locationExitVfx;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.GetComponent<Player>()) {
            var player = FindObjectOfType<Player>();
            _locationExitVfx.transform.position = player.transform.position;
            _locationExitVfx.LoadLocation(_nextLocation);
        }
    }
}
