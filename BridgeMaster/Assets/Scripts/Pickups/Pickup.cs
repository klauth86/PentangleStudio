using UnityEngine;

public class Pickup : ObjectWithEffects {
    [SerializeField] private ResourceUnit _resource;
    [SerializeField] private Collider2D _collider;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (_collider.IsTouchingLayers(LayerMask.GetMask("Player"))) {
            PlayEffects();
            GameSingleton.Singleton.Pickup(_resource);
            Destroy(gameObject);
        }            
    }
}
