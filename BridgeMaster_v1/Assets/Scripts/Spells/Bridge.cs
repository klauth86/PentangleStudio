using UnityEngine;

public class Bridge : ObjectWithEffects {
    [SerializeField] private Vector2 _offset;
    public Vector2 Offset { get { return _offset; } }

    private void Start() {
        PlayEffects();
    }
}
