using UnityEngine;

public class Background : MonoBehaviour {
    // Use this for initialization
    void Awake() {
        transform.localScale = new Vector3(2 * Camera.main.orthographicSize * Camera.main.aspect * transform.localScale.x,
            2 * Camera.main.orthographicSize * transform.localScale.y,
            transform.localScale.z);
    }
}
