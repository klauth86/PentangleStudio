using UnityEngine;

[CreateAssetMenu(menuName = "BridgeUnit")]
public class BridgeUnit : ScriptableObject {
    [SerializeField] private float _radius;
    [SerializeField] private float _width;
    [SerializeField] private float _height;

    public float Radius {
        get { return _radius; }
        set { _radius = value; }
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
