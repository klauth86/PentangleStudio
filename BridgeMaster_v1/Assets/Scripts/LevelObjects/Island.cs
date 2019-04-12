using System.Collections;
using UnityEngine;

public class Island : MonoBehaviour {
    
    [SerializeField] private Transform[] _markers;
    [SerializeField] private float _fluctuations;
    [SerializeField] private float _velocity;

    public Transform[] Markers {
        get {
            return _markers;
        }
    }

    private void Start() { }

    private void Update() {
        //StartCoroutine(Quake());        
    }

    //IEnumerable Quake() {
    //    var fluctuations = Random.insideUnitCircle * _fluctuations;
    //    var velocity = new Vector2(fluctuations.x - transform.position.x,
    //        fluctuations.y - transform.position.y);
    //    transform.Translate(fluctuations.x, fluctuations.y, 0);
    //}
}
