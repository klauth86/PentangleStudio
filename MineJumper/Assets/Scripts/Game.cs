using System;
using System.Collections;
using Base;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Game : MonoBehaviour {

    [SerializeField] private GameObject _gameCardPrefab;
    [SerializeField] private GameObject _touchPlane;
    [SerializeField] private Player _player;

    [SerializeField] private int _size;
    [SerializeField] private int _bombs;
    [SerializeField] private float _scaleFactor;

    [SerializeField] private float _coroutineTimeStep;
    [SerializeField] private float _axisTimeSensitivity;

    // Use this for initialization
    private void Start() {
        var board = new Board(2, _size, _bombs);
        var gameBoard = CreateBoard(board);
        AdjustCamera90(board);
        AdjustTouchPlane(board);
        StartCoroutine(PlayerTurnRoutine(gameBoard, board.Size));
    }

    private GameCard[,] CreateBoard(Board board) {
        var gameBoard = new GameCard[board.Size, board.Size];
        var offset = board.Size % 2 == 0 ? 0.5f : 0.0f;
        for (int i = 0; i < board.BoardSize; i++) {
            var gameCard = Instantiate(_gameCardPrefab,
                new Vector3(_scaleFactor * (i % board.Size - board.Size / 2 + offset), 0,
                _scaleFactor * (i / board.Size - board.Size / 2 + offset)), Quaternion.identity, transform).GetComponent<GameCard>();
            gameCard.Card = board.Cards[i];
            gameCard.OnSelectionChanged += OnSelectionChanged;
            gameBoard[i % board.Size, i / board.Size] = gameCard;
        }
        return gameBoard;
    }

    private void AdjustCamera45(Board board) {
        var offset = _scaleFactor * board.Size / 2 * (1 + Mathf.Tan(Mathf.PI / 12)) + 0.5f;
        Camera.main.transform.position = new Vector3(0, offset, -offset);
        Camera.main.transform.LookAt(Vector3.zero);
    }

    private void AdjustCamera90(Board board) {
        var offset = _scaleFactor * board.Size / 2 / Mathf.Tan(Mathf.PI / 6) + 0.5f;
        Camera.main.transform.position = new Vector3(0, offset, 0);
        Camera.main.transform.LookAt(Vector3.zero);
    }

    private void AdjustTouchPlane(Board board) {
        var scaleInUnits = _scaleFactor*board.Size;
        _touchPlane.transform.localScale = new Vector3(scaleInUnits,
            _touchPlane.transform.localScale.y,
            scaleInUnits);
    }

    private IEnumerator PlayerTurnRoutine(GameCard[,] gameBoard, int size) {
        _player.Freeze();

        var i = size / 2;
        var j = size / 2;
        gameBoard[i, j].IsSelected = true;

        var endTurn = false;
        float keyTimeout = 0;

        while(!endTurn) {
            yield return new WaitForSeconds(_coroutineTimeStep);
            var x = CrossPlatformInputManager.GetAxisRaw("Horizontal");
            var y = CrossPlatformInputManager.GetAxisRaw("Vertical");

            if (x != 0 || y != 0) {
                keyTimeout -= Time.deltaTime;
                if (x != 0 && i + (int)Mathf.Sign(x) >=0 && i + (int)Mathf.Sign(x) < size && keyTimeout <=0) {
                    keyTimeout = _axisTimeSensitivity;
                    i = i + (int)Mathf.Sign(x);
                }
                if (y != 0 && j + (int)Mathf.Sign(y) >=0 && j + (int)Mathf.Sign(y) < size && keyTimeout <= 0) {
                    keyTimeout = _axisTimeSensitivity;
                    j = j + (int)Mathf.Sign(y);
                }

                GameCard.SelectedCard.IsSelected = false;
                gameBoard[i, j].IsSelected = true;
            }
            else {
                keyTimeout = 0;
            }

            if (Input.touchCount > 0) {
                var ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit)) {
                    var gameCard = hit.collider.GetComponent<GameCard>();
                    if (gameCard && GameCard.SelectedCard != gameCard) {

                        GameCard.SelectedCard.IsSelected = false;
                        gameCard.IsSelected = true;
                    }
                }
                Debug.Log(ray);
            }
            endTurn = CrossPlatformInputManager.GetButton("Jump");
        }

        _player.Unfreeze();
    }

    private void OnSelectionChanged(GameCard card, bool isSelected) {
        //StartCoroutine(CardScaleRoutine(card, isSelected));
    }

    //private IEnumerator CardScaleRoutine(GameCard card, bool isSelected) {
    //    var to = isSelected ? 1.5f : 1.0f;
    //    var iterationCount = 40;
    //    for (int i = 1; i <= iterationCount; i++) {
    //        yield return new WaitForSeconds(0.0125f);
    //        card.transform.localScale = Vector3.one * (1.0f * (iterationCount - i) / iterationCount + to * i / iterationCount);
    //    }
    //}
}
