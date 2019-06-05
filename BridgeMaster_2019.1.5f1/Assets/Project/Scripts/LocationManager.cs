using UnityEngine;

public class LocationManager : MonoBehaviour {

    public static LocationManager Instance;

    // Use this for initialization
    void Start() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
