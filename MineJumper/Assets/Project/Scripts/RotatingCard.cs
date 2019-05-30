using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(MeshRenderer))]
public class RotatingCard : MonoBehaviour {

    public bool IsRotating;

    [SerializeField] private float _rotationVelocity;
    private Vector3 _rotationVector;

    private MeshRenderer _meshRenderer;
    protected MeshRenderer MeshRenderer {
        get {
            return _meshRenderer ?? (_meshRenderer = GetComponent<MeshRenderer>());
        }
    }

    // Use this for initialization
    void Start () { Init(); }

    protected virtual void Init() {
        _rotationVector = new Vector3(Random.Range(0.0f, 1.0f),
            Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f)).normalized;
    }
	
	// Update is called once per frame
	void Update () {
        if (IsRotating)
            transform.Rotate(_rotationVector * Time.deltaTime * _rotationVelocity);
    }

    internal void ChangeState(bool isMarking) {
        IsRotating = isMarking;
        MeshRenderer.material.color = isMarking ? Color.white : Color.gray;
    }
}
