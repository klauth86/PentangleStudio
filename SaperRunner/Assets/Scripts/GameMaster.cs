using UnityEngine;

public class GameMaster : MonoBehaviour {
    
    public static GameMaster Instance { get; set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    public void NewGame() {

    }
}
