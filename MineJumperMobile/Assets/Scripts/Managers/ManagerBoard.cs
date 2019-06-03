using Cards;
using Core;
using Events;
using System.Collections;
using UnityEngine;

namespace Managers {
    public class ManagerBoard : Base {

        #region Inspector

        [SerializeField] private MarkingCard _markedCard;
        [SerializeField] private GameObject _gameCardPrefab;

        [SerializeField] private float _scaleFactor;
        [SerializeField] private float _coroutineTimeStep;
        [SerializeField] private float _axisTimeSensitivity;

        #endregion

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

        private void AdjustCamera90(Board board) {
            var offset = _scaleFactor * board.Size / 2 / Mathf.Tan(Mathf.PI / 6) + 2.5f;
            if (Camera.main.aspect < 1)
                offset /= Camera.main.aspect;
            Camera.main.transform.position = new Vector3(0, offset, 0);
            Camera.main.transform.LookAt(Vector3.zero);
        }

        private IEnumerator PlayerTurnRoutine(Board board) {
            float inputTimeout = 0;
            bool isMark = false;

            while (gameObject.activeSelf) {
                yield return new WaitForSeconds(_coroutineTimeStep);
                inputTimeout -= Time.deltaTime;

                if (ProcessTouchInput(board, ref inputTimeout, ref isMark))
                    inputTimeout = 0;
            }
        }

        private bool ProcessTouchInput(Board board, ref float inputTimeout, ref bool isMark) {
            if (Input.touchCount > 0) {
                var ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit)) {
                    if (inputTimeout <= 0) {
                        inputTimeout = _axisTimeSensitivity;

                        var gameCard = hit.collider.GetComponent<GameCard>();
                        if (gameCard) {
                            if (isMark)
                                board.MarkCard(gameCard.Card);
                            else
                                board.RevealCard(gameCard.Card);
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

        private void OnStatusChanged(Board board, BoardStatus status) {
            GameStatusChanged(status);
        }

        #region Events

        public event GameAction<BoardStatus> GameStatusChanged = delegate { };

        #endregion

        #region Manager Methods

        public void CreateBoard(int size, int bombs) {
            var board = new Board(2, size, bombs);
            CreateBoard(board);
            AdjustCamera90(board);
            StartCoroutine(PlayerTurnRoutine(board));
        }

        #endregion
    }
}
