using UnityEngine;

public class RotatingCard : MonoBehaviour {

    [SerializeField] private float _rotationVelocity;
    private Vector3 _rotationVector;

    // Use this for initialization
    void Start () {
        _rotationVector = new Vector3(
        Random.Range(0.0f, 1.0f),
        Random.Range(0.0f, 1.0f),
        Random.Range(0.0f, 1.0f)).normalized;
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(_rotationVector * Time.deltaTime * _rotationVelocity);
    }
}
