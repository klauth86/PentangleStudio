using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {
    
    public static GameMaster Instance { get; set; }

    [SerializeField] private Slider _sizeSlider;
    [SerializeField] private Slider _pandoraSlider;
    [SerializeField] private Text _startButtonText;

    [SerializeField] private GameBoard _gameBoardPrefab;
    [SerializeField] private Card _cardPrefab;

    private GameMasterBase _master = new GameMasterBase();

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

    private void Update() {
        var size = GetSize();
        var pandora = GetPandora();
        _startButtonText.text = "Start (" + size.ToString() + ":" + pandora.ToString() + ")";
    }

    private float GetPandora() {
        return _pandoraSlider.value;
    }

    private float GetSize() {
        return _sizeSlider.value;
    }

    public void NewGame() {
        _master.StartGame(GetPandora(), (int)GetSize(), new RectangleLayout(_cardPrefab));
    }
}
