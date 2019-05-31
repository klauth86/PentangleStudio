using System;
using Base;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    public static LevelManager Instance;

    [SerializeField] public InputDevice InputDevice;
    [SerializeField] public int Size;
    [SerializeField] public int Bombs;

    [SerializeField] private Game _gamePrefab;

    [SerializeField] private GameObject _menuPanel;

    [SerializeField] private GameObject _gamePanel;
    [SerializeField] private TMPro.TMP_Text _text;

    [SerializeField] private Image _keyboardButtonImage;
    [SerializeField] private Image _mouseButtonImage;
    [SerializeField] private Image _touchButtonImage;

    [SerializeField] private GameObject _playButton;

    private InputDevice _selectedInputDevice;

    // Use this for initialization
    void Start() {
        Instance = this;
    }

    internal void OnBoardStatusChanged(BoardStatus status) {
        InputDevice = InputDevice.None;
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

    public void LoadMenu() {
        DestroyPrevGame();

        _text.text = "";
        _gamePanel.SetActive(false);

        _menuPanel.SetActive(true);
    }

    public void LoadLevel() {
        DestroyPrevGame();

        UpdateInputDevice();

        _text.text = "";
        _gamePanel.SetActive(false);

        _menuPanel.SetActive(false);

        Instantiate(_gamePrefab);
    }

    public void SetKeyboardDevice() {
        ResetColors();
        _keyboardButtonImage.color = new Color(0, 1, 0, 0.25f);
        _selectedInputDevice = InputDevice.Keyboard;
        _playButton.SetActive(true);
    }

    public void SetMouseDevice() {
        ResetColors();
        _mouseButtonImage.color = new Color(0, 1, 0, 0.25f);
        _selectedInputDevice = InputDevice.Mouse;
        _playButton.SetActive(true);
    }

    public void SetTouchDevice() {
        ResetColors();
        _touchButtonImage.color = new Color(0, 1, 0, 0.25f);
        _selectedInputDevice = InputDevice.Touch;
        _playButton.SetActive(true);
    }

    public void Exit() {
        Application.Quit();
    }
}
