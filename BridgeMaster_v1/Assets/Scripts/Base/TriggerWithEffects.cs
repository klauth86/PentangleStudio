using UnityEngine;

public abstract class TriggerWithEffects : ObjectWithEffects {

    [SerializeField] protected Collider2D _collider;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (_collider.IsTouchingLayers(LayerMask.GetMask(Layers.PlayerLayer))) {
            TriggeredAction();
            PlayEffects();
        }
    }

    protected abstract void TriggeredAction();
}
