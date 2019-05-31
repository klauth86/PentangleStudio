using Base;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public static LevelManager Instance;
    [SerializeField] public BoardStatus BoardStatus;
    [SerializeField] public InputDevice InputDevice;
    [SerializeField] public int Size;
    [SerializeField] public int Bombs;

    [SerializeField] private GameObject _panel;
    [SerializeField] private TMPro.TMP_Text _text;


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

    internal void OnBoardStatusChanged(BoardStatus status) {
        BoardStatus = status;
        _text.text = status.ToString();
        _panel.SetActive(true);
    }

    public void LoadMenu() {
        SceneManager.LoadScene(0);
    }

    public void LoadLevel() {
        SceneManager.LoadScene(1);
    }
}
