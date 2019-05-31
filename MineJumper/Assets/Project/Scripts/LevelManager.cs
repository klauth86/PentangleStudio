using Base;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public static LevelManager Instance;
    [SerializeField] public BoardStatus BoardStatus;
    [SerializeField] public InputDevice InputDevice;
    [SerializeField] public int Size;
    [SerializeField] public int Bombs;

    [SerializeField] private GameObject _menuPanel;

    [SerializeField] private GameObject _gamePanel;
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
        _text.text = GetDescription(status);
        _gamePanel.SetActive(true);
    }

    public void LoadMenu() {
        _text.text = "";
        _gamePanel.SetActive(false);
        SceneManager.LoadScene(0);
    }

    public void LoadLevel() {
        _text.text = "";
        _gamePanel.SetActive(false);
        BoardStatus = BoardStatus.Active;
        SceneManager.LoadScene(1);
    }

    public string GetDescription(BoardStatus status) {
        switch (status) {
            case BoardStatus.Lose:
                return "You LOSE!!!";
            case BoardStatus.Win:
                return "You WIN!!!";
        }
        return "";
    }
}
