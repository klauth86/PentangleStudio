using System.Collections;
using System.Linq;
using Base;
using Cards;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Game : MonoBehaviour {

    [SerializeField] private GameObject _gameCardPrefab;
    [SerializeField] private MarkingCard _markedCard;

    [SerializeField] private float _scaleFactor;
    [SerializeField] private float _coroutineTimeStep;
    [SerializeField] private float _axisTimeSensitivity;

    // Use this for initialization
    private void Start() {
        var board = new Board(2, LevelManager.Instance.Size, LevelManager.Instance.Bombs);
        board.OnBoardStatusChanged += OnBoardStatusChanged;
        var gameBoard = CreateBoard(board);
        AdjustCamera90();
        StartCoroutine(PlayerTurnRoutine(gameBoard));
    }

    private GameCard[] CreateBoard(Board board) {
        var notTouch = LevelManager.Instance.InputDevice != InputDevice.Touch;
        if (notTouch)
            Destroy(_markedCard.gameObject);
        else
            _markedCard.ChangeState(false);
        

        var gameBoard = new GameCard[board.BoardSize];
        var offset = board.Size % 2 == 0 ? 0.5f : 0.0f;
        for (int i = 0; i < board.BoardSize; i++) {
            var gameCard = Instantiate(_gameCardPrefab,
                new Vector3(_scaleFactor * (i % board.Size - board.Size / 2 + offset + (notTouch ? 0 : 1)), 0,
                _scaleFactor * (i / board.Size - board.Size / 2 + offset)), Quaternion.identity, transform).GetComponent<GameCard>();
            gameCard.Card = board.Cards[i];
            gameBoard[i] = gameCard;
        }

        for (int i = 0; i < board.BoardSize; i++) {
            if (i - board.Size >= 0) {
                gameBoard[i].down = gameBoard[i - board.Size];
            }
            if (i + board.Size < board.BoardSize) {
                gameBoard[i].up = gameBoard[i + board.Size];
            }
            if (i - 1 >= 0 && (i - 1) / board.Size == i / board.Size) {
                gameBoard[i].left = gameBoard[i - 1];
            }
            if (i + 1 < board.BoardSize && (i + 1) / board.Size == i / board.Size) {
                gameBoard[i].right = gameBoard[i + 1];
            }
        }
        return gameBoard;
    }

    private void AdjustCamera90() {
        var offset = _scaleFactor * LevelManager.Instance.Size / 2 / Mathf.Tan(Mathf.PI / 6) + 2.5f;
        if (Camera.main.aspect < 1)
            offset /= Camera.main.aspect;
        Camera.main.transform.position = new Vector3(0, offset, 0);
        Camera.main.transform.LookAt(Vector3.zero);
    }

    private IEnumerator PlayerTurnRoutine(GameCard[] gameBoard) {
        float inputTimeout = 0;
        bool isMarking = false;
        GameCard selected = null;

        while (gameObject.activeSelf) {
            yield return new WaitForSeconds(_coroutineTimeStep);
            inputTimeout -= Time.deltaTime;

            bool resetInputTimeout = false;

            if (InputDevice.Keyboard == LevelManager.Instance.InputDevice)
                resetInputTimeout = resetInputTimeout || ProcessKeyboardInput(ref inputTimeout, gameBoard, ref selected);

            if (InputDevice.Touch == LevelManager.Instance.InputDevice) {
                resetInputTimeout = resetInputTimeout || ProcessTouchInput(ref inputTimeout, ref isMarking);
            }

            if (resetInputTimeout)
                inputTimeout = 0;
        }
    }

    private bool ProcessTouchInput(ref float inputTimeout, ref bool isMarking) {
        if (Input.touchCount > 0) {
            var ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                if (inputTimeout <= 0) {
                    inputTimeout = _axisTimeSensitivity;

                    var gameCard = hit.collider.GetComponent<GameCard>();
                    if (gameCard) {
                        if (isMarking)
                            gameCard.Card.Mark();
                        else
                            gameCard.Card.Reveal();
                    }
                    else {
                        isMarking = !isMarking;
                        _markedCard.ChangeState(isMarking);
                    }
                }
                return false;
            }
        }
        return true;
    }

    private bool ProcessKeyboardInput(ref float inputTimeout, GameCard[] gameBoard, ref GameCard selected) {
        var x = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        var y = CrossPlatformInputManager.GetAxisRaw("Vertical");
        var fire = CrossPlatformInputManager.GetButton("Fire1");

        if (x != 0 || y != 0 || fire) {
            if (inputTimeout <= 0) {
                inputTimeout = _axisTimeSensitivity;

                if (selected == null) {
                    selected = gameBoard.First(item => item != null);
                }
                else {
                    if (x != 0) {
                        selected.SelectionObject.SetActive(false);
                        selected = (x > 0 ? selected.right : selected.left) ?? selected;
                        selected.SelectionObject.SetActive(true);
                    }
                    else if (y != 0) {
                        selected.SelectionObject.SetActive(false);
                        selected = (y > 0 ? selected.up : selected.down) ?? selected;
                        selected.SelectionObject.SetActive(true);
                    }
                    else if (CrossPlatformInputManager.GetButton("Fire1")) {
                        selected.Card.Mark();
                    }
                }
            }
            return false;            
        }
        if (selected != null && CrossPlatformInputManager.GetButton("Jump")) {
            if (selected.Card.Reveal())
                selected = null;
        }
        return true;
    }

    internal void OnBoardStatusChanged(Board board, BoardStatus status) {
        board.OnBoardStatusChanged -= OnBoardStatusChanged;
        LevelManager.Instance.OnBoardStatusChanged(status);
    }
}
