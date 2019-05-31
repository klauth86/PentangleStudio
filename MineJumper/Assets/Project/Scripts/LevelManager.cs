using System;
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

    // Use this for initialization
    void Start() {
        Instance = this;
    }

    internal void OnBoardStatusChanged(BoardStatus status) {
        _text.text = GetBoardStatusDescription(status);
        _gamePanel.SetActive(true);
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

    private void UpdateInputDevice() {
        throw new NotImplementedException();
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
}
