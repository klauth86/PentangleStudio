using Cards;
using Core;
using System.Collections;
using UnityEngine;

namespace Managers {
    public class ManagerBoard : Base {

        [SerializeField] private MarkingCard _markedCard;
        [SerializeField] private GameObject _gameCardPrefab;

        [SerializeField] private float _scaleFactor;
        [SerializeField] private float _coroutineTimeStep;
        [SerializeField] private float _axisTimeSensitivity;

        private GameCard[] CreateBoard(Board board) {
            _markedCard.ChangeState(false);

            var gameBoard = new GameCard[board.BoardSize];
            var offset = board.Size % 2 == 0 ? 0.5f : 0.0f;
            for (int i = 0; i < board.BoardSize; i++) {
                var gameCard = Instantiate(_gameCardPrefab,
                    new Vector3(_scaleFactor * (i % board.Size - board.Size / 2 + offset + 1), 0,
                    _scaleFactor * (i / board.Size - board.Size / 2 + offset)), Quaternion.identity, transform).GetComponent<GameCard>();



                gameCard.Card = board.Cards[i];
                gameBoard[i] = gameCard;
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
            bool isMark = false;

            while (gameObject.activeSelf) {
                yield return new WaitForSeconds(_coroutineTimeStep);
                inputTimeout -= Time.deltaTime;

                if (ProcessTouchInput(ref inputTimeout, ref isMark))
                    inputTimeout = 0;
            }
        }

        private bool ProcessTouchInput(ref float inputTimeout, ref bool isMark) {
            if (Input.touchCount > 0) {
                var ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit)) {
                    if (inputTimeout <= 0) {
                        inputTimeout = _axisTimeSensitivity;

                        var gameCard = hit.collider.GetComponent<GameCard>();
                        if (gameCard) {
                            if (isMark)
                                gameCard.Card.Mark(LevelManager.Instance.BombsLeft);
                            else
                                gameCard.Card.Reveal();
                        }
                        else {
                            isMark = !isMark;
                            _markedCard.ChangeState(isMark);
                        }
                    }
                    return false;
                }
            }
            return true;
        }

        private void OnBoardStatusChanged(Board board, BoardStatus status) {
            board.MarkedCardsChanged -= OnMarkedCardsChanged;
            board.StatusChanged -= OnBoardStatusChanged;
            LevelManager.Instance.OnBoardStatusChanged(status);
        }

        private void OnMarkedCardsChanged(int count) {
            LevelManager.Instance.UpdateBombsLeft(count);
        }

        #region Manager Methods

        public void CreateBoard(int size, int bombs) {
            var board = new Board(2, size, bombs);

            board.MarkedCardsChanged += OnMarkedCardsChanged;
            board.StatusChanged += OnBoardStatusChanged;

            var gameBoard = CreateBoard(board);
            AdjustCamera90();
            StartCoroutine(PlayerTurnRoutine(gameBoard));
        }

        #endregion
    }
}
