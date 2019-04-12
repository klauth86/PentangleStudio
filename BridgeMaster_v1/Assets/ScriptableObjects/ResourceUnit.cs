using UnityEngine;

[CreateAssetMenu(menuName = "ResourseUnit")]
public class ResourceUnit : ScriptableObject {
    [SerializeField] private float _mana;
    [SerializeField] private float _ore;
    [SerializeField] private float _wood;
    [SerializeField] private float _rope;

    public float Mana {
        get { return _mana; }
        set { _mana = value; }
    }

    public float Ore {
        get { return _ore; }
        set { _ore = value; }
    }

    public float Wood {
        get { return _wood; }
        set { _wood = value; }
    }

    public float Rope {
        get { return _rope; }
        set { _rope = value; }
    }
}