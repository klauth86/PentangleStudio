using System;
using Base;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    public static LevelManager Instance;

    [SerializeField] public InputDevice InputDevice;
    [SerializeField] public int Size;
    [SerializeField] public int Bombs;

    public int BombsLeft;

    [SerializeField] private Game _gamePrefab;

    [SerializeField] private GameObject _menuPanel;

    [SerializeField] private GameObject _gamePanel;
    [SerializeField] private TMPro.TMP_Text _bombsLeftText;

    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private TMPro.TMP_Text _statusText;

    [SerializeField] private Image _keyboardButtonImage;
    [SerializeField] private Image _mouseButtonImage;
    [SerializeField] private Image _touchButtonImage;

    [SerializeField] private GameObject _playButton;

    [SerializeField] private TMPro.TMP_Text _helpText;

    private InputDevice _selectedInputDevice;

    public AudioManager AudioManager;

    // Use this for initialization
    void Start() {
        Instance = this;
        AudioManager = GetComponent<AudioManager>();
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

    private string GetInputDeviceDescription(InputDevice device) {
        switch (device) {
            case InputDevice.Keyboard:
                return "ARROWS to move selection," + Environment.NewLine + "CTRL to mark as bomb," + Environment.NewLine + "SPACE to reveal ...";
            case InputDevice.Touch:
                return "TOUCH to reveal," + Environment.NewLine + "TOUCH to mark as bomb if mark button is pressed ...";
            case InputDevice.Mouse:
                return "LEFT CLICK to reveal," + Environment.NewLine + "RIGHT CLICK to mark as bomb ...";
        }
        return "";
    }

    private void DestroyPrevGame() {
        var prevGame = FindObjectOfType<Game>();
        if (prevGame)
            Destroy(prevGame.gameObject);
    }

    private void ResetColors() {;
        _keyboardButtonImage.color = new Color(1, 1, 1, 0.25f);
        _mouseButtonImage.color = new Color(1, 1, 1, 0.25f);
        _touchButtonImage.color = new Color(1, 1, 1, 0.25f);
    }

    public void UpdateBombsLeft(int left) {
        BombsLeft = left;
        _bombsLeftText.text = left.ToString();
    }

    public void OnBoardStatusChanged(BoardStatus status) {
        InputDevice = InputDevice.None;
        _statusText.text = GetBoardStatusDescription(status);
        _gameOverPanel.SetActive(true);

        if (status == BoardStatus.Win)
            AudioManager.PlayWinAudio();

        if (status == BoardStatus.Lose)
            AudioManager.PlayWinAudio();
    }

    #region Button click

    public void LoadMenu() {
        AudioManager.PlayButtonAudio();
        DestroyPrevGame();

        _statusText.text = "";
        _gamePanel.SetActive(false);

        _menuPanel.SetActive(true);
    }

    public void LoadLevel() {
        AudioManager.PlayButtonAudio();
        DestroyPrevGame();

        UpdateInputDevice();

        UpdateBombsLeft(Bombs);

        _statusText.text = "";
        _gamePanel.SetActive(true);
        _gameOverPanel.SetActive(false);
        _menuPanel.SetActive(false);

        Instantiate(_gamePrefab);
    }

    public void SetKeyboardDevice() {
        AudioManager.PlayButtonAudio();
        ResetColors();
        _keyboardButtonImage.color = new Color(0, 1, 0, 0.25f);
        _selectedInputDevice = InputDevice.Keyboard;
        _helpText.text = GetInputDeviceDescription(_selectedInputDevice);
        _playButton.SetActive(true);
    }

    public void SetMouseDevice() {
        AudioManager.PlayButtonAudio();
        ResetColors();
        _mouseButtonImage.color = new Color(0, 1, 0, 0.25f);
        _selectedInputDevice = InputDevice.Mouse;
        _helpText.text = GetInputDeviceDescription(_selectedInputDevice);
        _playButton.SetActive(true);
    }

    public void SetTouchDevice() {
        AudioManager.PlayButtonAudio();
        ResetColors();
        _touchButtonImage.color = new Color(0, 1, 0, 0.25f);
        _selectedInputDevice = InputDevice.Touch;
        _helpText.text = GetInputDeviceDescription(_selectedInputDevice);
        _playButton.SetActive(true);
    }

    public void Exit() {
        AudioManager.PlayButtonAudio();
        Application.Quit();
    }

    #endregion
}
