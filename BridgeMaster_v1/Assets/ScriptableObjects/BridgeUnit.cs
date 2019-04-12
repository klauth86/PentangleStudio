using UnityEngine;

[CreateAssetMenu(menuName = "BridgeUnit")]
public class BridgeUnit : ScriptableObject {
    [SerializeField] private Vector2 _offset;
    [SerializeField] private float _width;
    [SerializeField] private float _height;

    public Vector2 Offset {
        get { return _offset; }
        set { _offset = value; }
    }

    public float Width {
        get { return _width; }
        set { _width = value; }
    }

    public float Height {
        get { return _height; }
        set { _height = value; }
    }
}
