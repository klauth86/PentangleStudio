using Base;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public static LevelManager Instance;

    [SerializeField] public InputDevice InputDevice;
    [SerializeField] public int Size;
    [SerializeField] public int Bombs;

    [SerializeField] private Game _gamePrefab;

    [SerializeField] private GameObject _menuPanel;

    [SerializeField] private GameObject _gamePanel;
    [SerializeField] private TMPro.TMP_Text _text;

    [SerializeField] private TMPro.TMP_Text _keyboardText;
    [SerializeField] private TMPro.TMP_Text _mouseText;
    [SerializeField] private TMPro.TMP_Text _touchText;

    [SerializeField] private GameObject _playButton;

    private InputDevice _selectedInputDevice;

    // Use this for initialization
    void Start() {
        Instance = this;
    }

    internal void OnBoardStatusChanged(BoardStatus status) {
        _text.text = GetBoardStatusDescription(status);
        _gamePanel.SetActive(true);
    }

    private void UpdateInputDevice() {
        InputDevice = _selectedInputDevice;
    }

    private string GetBoardStatusDescription(BoardStatus status) {
        switch (status) {
            case BoardStatus.Lose:
                return "You LOSE!!!";
            case BoardStatus.Win:
                return "You WIN!!!";
        }
        return "";
    }

    public void LoadMenu() {
        InputDevice = InputDevice.None;

        _text.text = "";
        _gamePanel.SetActive(false);

        _menuPanel.SetActive(true);
    }

    public void LoadLevel() {
        UpdateInputDevice();

        _text.text = "";
        _gamePanel.SetActive(false);

        _menuPanel.SetActive(false);

        Instantiate(_gamePrefab);
    }

    public void SetKeyboardDevice() {
        _keyboardText.color = Color.green;
        _selectedInputDevice = InputDevice.Keyboard;
        _playButton.SetActive(true);
    }

    public void SetMouseDevice() {
        _mouseText.color = Color.green;
        _selectedInputDevice = InputDevice.Mouse;
        _playButton.SetActive(true);
    }

    public void SetTouchDevice() {
        _touchText.color = Color.green;
        _selectedInputDevice = InputDevice.Touch;
        _playButton.SetActive(true);
    }

    public void Exit() {
        Application.Quit();
    }
}
