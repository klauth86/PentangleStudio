using UnityEngine;

public class Pickup : TriggerWithEffects {
    [SerializeField] private ResourceUnit _resource;

    protected override void TriggeredAction() {
        GameSingleton.Singleton.Pickup(_resource);
    }
}
