using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField]
    private float _velocity;

    private List<PickupableObject> _pickUps;

    internal void AddPickup(PickupableObject pickupableObject)
    {
        _pickUps.Add(pickupableObject);
    }

    // Use this for initialization
    void Start () {
        _pickUps = new List<PickupableObject>();
	}

    // Update is called once per frame
    void Update()
    {
        var k = 0;
        if (Input.GetKeyDown(KeyCode.RightArrow))
            k = 1;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            k = -1;
        transform.position = new Vector2(transform.position.x + _velocity * k * Time.deltaTime, transform.position.y);
    }
}
