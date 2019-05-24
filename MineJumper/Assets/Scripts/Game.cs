using System;
using System.Collections;
using Base;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Game : MonoBehaviour {

    [SerializeField] private GameObject _gameCardPrefab;
    [SerializeField] private GameObject _touchPlane;

    [SerializeField] private int _size;
    [SerializeField] private int _bombs;
    [SerializeField] private float _scaleFactor;

    // Use this for initialization
    private void Start() {
        var board = new Board(2, _size, _bombs);
        var gameBoard = CreateBoard(board);
        AdjustCamera(board);
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

    private void AdjustCamera(Board board) {
        var offset = _scaleFactor * board.Size / 2 * (1 + Mathf.Tan(Mathf.PI / 12)) + 0.5f;
        Camera.main.transform.position = new Vector3(0, offset, -offset);
    }

    private void AdjustTouchPlane(Board board) {
        var scaleInUnits = _scaleFactor*board.Size;
        _touchPlane.transform.localScale = new Vector3(scaleInUnits,
            _touchPlane.transform.localScale.y,
            scaleInUnits);
    }

    private IEnumerator PlayerTurnRoutine(GameCard[,] gameBoard, int size) {
        //Time.timeScale = 0.0f;

        var i = size / 2;
        var j = size / 2;
        gameBoard[i, j].IsSelected = true;

        var _endTurn = false;
        while(!_endTurn) {
            yield return new WaitForSeconds(0.5f);
            var x = CrossPlatformInputManager.GetAxis("Horizontal");
            var y = CrossPlatformInputManager.GetAxis("Vertical");
            if (x != 0 || y != 0) {
                gameBoard[i, j].IsSelected = false;
                i = i + (int)Mathf.Sign(x);
                j = j + (int)Mathf.Sign(y);
                gameBoard[i, j].IsSelected = true;
            }
            _endTurn = CrossPlatformInputManager.GetButton("Jump");
        }

        Time.timeScale = 1.0f;
    }

    private void OnSelectionChanged(GameCard card, bool isSelected) {
        card.transform.localScale = Vector3.Lerp(card.transform.localScale, card.transform.localScale * (isSelected ? 1.25f : 0.8f), 1.2f);
    }
}
