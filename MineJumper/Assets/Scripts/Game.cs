using System.Collections;
using Base;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Game : MonoBehaviour {

    [SerializeField] private GameObject _gameCardPrefab;
    [SerializeField] private GameObject _touchPlane;
    [SerializeField] private Player _player;

    [SerializeField] private float _scaleFactor;

    [SerializeField] private float _coroutineTimeStep;
    [SerializeField] private float _axisTimeSensitivity;

    // Use this for initialization
    private void Start() {
        CreateBoard(new Board(2, LevelManager.Instance.Size, LevelManager.Instance.Bombs));
        AdjustCamera90();
        AdjustTouchPlane();
        StartCoroutine(PlayerTurnRoutine());
    }

    private void CreateBoard(Board board) {
        var gameBoard = new GameCard[board.Size, board.Size];
        var offset = board.Size % 2 == 0 ? 0.5f : 0.0f;
        for (int i = 0; i < board.BoardSize; i++) {
            var gameCard = Instantiate(_gameCardPrefab,
                new Vector3(_scaleFactor * (i % board.Size - board.Size / 2 + offset), 0,
                _scaleFactor * (i / board.Size - board.Size / 2 + offset)), Quaternion.identity, transform).GetComponent<GameCard>();
            gameCard.Card = board.Cards[i];
            gameBoard[i % board.Size, i / board.Size] = gameCard;
        }
        return gameBoard;
    }

    //private void AdjustCamera45(Board board) {
    //    var offset = _scaleFactor * board.Size / 2 * (1 + Mathf.Tan(Mathf.PI / 12)) + 0.5f;
    //    Camera.main.transform.position = new Vector3(0, offset, -offset);
    //    Camera.main.transform.LookAt(Vector3.zero);
    //}

    private void AdjustCamera90() {
        var offset = _scaleFactor * LevelManager.Instance.Size / 2 / Mathf.Tan(Mathf.PI / 6) + 0.5f;
        Camera.main.transform.position = new Vector3(0, offset, 0);
        Camera.main.transform.LookAt(Vector3.zero);
    }

    private void AdjustTouchPlane() {
        var scaleInUnits = _scaleFactor * LevelManager.Instance.Size;
        _touchPlane.transform.localScale = new Vector3(scaleInUnits,
            _touchPlane.transform.localScale.y,
            scaleInUnits);
    }

    private IEnumerator PlayerTurnRoutine() {
        var i = 0;
        var j = 0;
        var hasSelectedGameCard = false;

        float keyTimeout = 0;
        while (true) {
            yield return new WaitForSeconds(_coroutineTimeStep);

            if (InputDevice.Keyboard == LevelManager.Instance.InputDevice) {
                var x = CrossPlatformInputManager.GetAxisRaw("Horizontal");
                var y = CrossPlatformInputManager.GetAxisRaw("Vertical");

                if (x != 0 || y != 0) {
                    keyTimeout -= Time.deltaTime;
                    if (x != 0 && i + (int)Mathf.Sign(x) >= 0 && i + (int)Mathf.Sign(x) < size && keyTimeout <= 0) {
                        keyTimeout = _axisTimeSensitivity;
                        gameBoard[i, j].SelectionObject.SetActive(false);
                        var dx = (int)Mathf.Sign(x);
                        i = hasSelectedGameCard ? i + dx : (dx + size) % size;
                    }
                    if (y != 0 && j + (int)Mathf.Sign(y) >= 0 && j + (int)Mathf.Sign(y) < size && keyTimeout <= 0) {
                        keyTimeout = _axisTimeSensitivity;
                        gameBoard[i, j].SelectionObject.SetActive(false);
                        j = j + (int)Mathf.Sign(y);
                    }
                    gameBoard[i, j].SelectionObject.SetActive(true);
                }
                else {
                    keyTimeout = 0;
                }

                if (CrossPlatformInputManager.GetButton("Jump")) {
                    gameBoard[i, j].Card.Reveal();
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
    }
}
