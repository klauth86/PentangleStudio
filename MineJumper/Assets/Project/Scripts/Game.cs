using System.Collections;
using System.Linq;
using Base;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Game : MonoBehaviour {

    [SerializeField] private GameObject _gameCardPrefab;
    [SerializeField] private GameObject _bombCardPrefab;
    [SerializeField] private GameObject _markedCardPrefab;

    [SerializeField] private GameObject _touchPlane;
    [SerializeField] private Player _player;

    [SerializeField] private float _scaleFactor;

    [SerializeField] private float _coroutineTimeStep;
    [SerializeField] private float _axisTimeSensitivity;

    // Use this for initialization
    private void Start() {
        var board = new Board(2, LevelManager.Instance.Size, LevelManager.Instance.Bombs);
        board.OnBoardStatusChanged += LevelManager.Instance.OnBoardStatusChanged;

        var gameBoard = CreateBoard(board);
        AdjustCamera90();
        AdjustTouchPlane();
        StartCoroutine(PlayerTurnRoutine(gameBoard));
    }

    private GameCard[] CreateBoard(Board board) {
        var gameBoard = new GameCard[board.BoardSize];
        var offset = board.Size % 2 == 0 ? 0.5f : 0.0f;
        for (int i = 0; i < board.BoardSize; i++) {
            var gameCard = Instantiate(_gameCardPrefab,
                new Vector3(_scaleFactor * (i % board.Size - board.Size / 2 + offset + 1), 0,
                _scaleFactor * (i / board.Size - board.Size / 2 + offset)), Quaternion.identity, transform).GetComponent<GameCard>();
            gameCard.Card = board.Cards[i];
            gameBoard[i] = gameCard;
        }

        if (LevelManager.Instance.InputDevice == InputDevice.Touch) {
            var marked = Instantiate(_markedCardPrefab, new Vector3(0, 0, 0), Quaternion.identity, transform);
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

    //private void AdjustCamera45(Board board) {
    //    var offset = _scaleFactor * board.Size / 2 * (1 + Mathf.Tan(Mathf.PI / 12)) + 0.5f;
    //    Camera.main.transform.position = new Vector3(0, offset, -offset);
    //    Camera.main.transform.LookAt(Vector3.zero);
    //}

    private void AdjustCamera90() {
        var offset = _scaleFactor * LevelManager.Instance.Size / 2 / Mathf.Tan(Mathf.PI / 6) + 2.5f;
        if (Camera.main.aspect < 1)
            offset /= Camera.main.aspect;
        Camera.main.transform.position = new Vector3(0, offset, 0);
        Camera.main.transform.LookAt(Vector3.zero);
    }

    private void AdjustTouchPlane() {
        var scaleInUnits = _scaleFactor * LevelManager.Instance.Size;
        _touchPlane.transform.localScale = new Vector3(scaleInUnits,
            _touchPlane.transform.localScale.y,
            scaleInUnits);
    }

    private IEnumerator PlayerTurnRoutine(GameCard[] gameBoard) {
        GameCard selected = null;
        float keyTimeout = 0;

        while (LevelManager.Instance.BoardStatus == BoardStatus.Active) {
            yield return new WaitForSeconds(_coroutineTimeStep);

            if (InputDevice.Keyboard == LevelManager.Instance.InputDevice) {
                keyTimeout -= Time.deltaTime;

                var x = CrossPlatformInputManager.GetAxisRaw("Horizontal");
                var y = CrossPlatformInputManager.GetAxisRaw("Vertical");
                var fire = CrossPlatformInputManager.GetButton("Fire1");

                if (x != 0 || y != 0 || fire) {
                    if (keyTimeout <= 0) {
                        keyTimeout = _axisTimeSensitivity;

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
                }
                else {
                    keyTimeout = 0;
                }

                if (selected != null && CrossPlatformInputManager.GetButton("Jump")) {
                    if (selected.Card.Reveal())
                        selected = null;
                }
            }           

            if (InputDevice.Touch == LevelManager.Instance.InputDevice && Input.touchCount > 0) {
                var ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit)) {
                    var gameCard = hit.collider.GetComponent<GameCard>();
                    if (gameCard) {
                        gameCard.Card.Reveal();
                    }
                }
                Debug.Log(ray);
            }
        }
        if (selected != null)
            selected.SelectionObject.SetActive(false);
    }
}
