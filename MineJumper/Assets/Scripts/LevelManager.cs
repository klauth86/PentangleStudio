using UnityEngine;

public class LevelManager : MonoBehaviour {

    public static LevelManager Instance;

    public InputDevice InputDevice { get; set; }

    // Use this for initialization
    void Start () {
		if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
