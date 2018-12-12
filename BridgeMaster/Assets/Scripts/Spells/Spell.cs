using UnityEngine;

public class Spell : ObjectWithEffects {
    [SerializeField] private ResourceUnit _cost;
    [SerializeField] private BridgeUnit _bridge;

    public ResourceUnit Cost {
        get { return _cost; }
    }

    public Vector2 Cast() {
        PlayEffects();
        //CreateBridgeSegment(_bridge);
        return Vector2.right;
    }
}