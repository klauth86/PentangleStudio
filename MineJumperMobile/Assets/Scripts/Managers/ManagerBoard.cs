using Cards;
using Core;
using Events;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Managers {
    public class ManagerBoard : Base {

        #region Inspector

        [SerializeField] private GameObject _markingCardPrefab;
        [SerializeField] private GameObject _gameCardPrefab;

        [SerializeField] private float _scaleFactor;
        [SerializeField] private float _coroutineTimeStep;
        [SerializeField] private float _axisTimeSensitivity;

        #endregion

        private Dictionary<GameCard, Card> CreateGameBoard(Board board) {
            var gameCards = new Dictionary<GameCard, Card>(board.BoardSize);

            #region GameBoard

            var offset = board.Size % 2 == 0 ? 0.5f : 0.0f;
            for (int i = 0; i < board.BoardSize; i++) {
                var gameCard = Instantiate(_gameCardPrefab,
                    new Vector3(_scaleFactor * (i % board.Size - board.Size / 2 + offset + 1), 0,
                    _scaleFactor * (i / board.Size - board.Size / 2 + offset)), Quaternion.identity).GetComponent<GameCard>();
                gameCards.Add(gameCard, board.Cards[i]);
            }

            #endregion

            #region Camera

            var cameraOffset = _scaleFactor * board.Size / 2 / Mathf.Tan(Mathf.PI / 6) + 2.5f;
            if (Camera.main.aspect < 1)
                cameraOffset /= Camera.main.aspect;
            Camera.main.transform.position = new Vector3(0, cameraOffset, 0);
            Camera.main.transform.LookAt(Vector3.zero);

            var markingCard = Instantiate(_markingCardPrefab, new Vector3(-_scaleFactor * board.Size / 2, cameraOffset - 4, 0), Quaternion.identity);

            #endregion

            #region Subscribtions

            GameAction<Board, Card> onCardMarked = (b, c) => {
                var gameCard = gameCards.First(pair => pair.Value == c).Key;
                gameCard.Mark(c.IsMarked);
            };

            GameAction<Board, Card> onCardRevealed = (b, c) => {
                var gameCard = gameCards.First(pair => pair.Value == c).Key;
                gameCard.Reveal(c.BombIndex);
            };

            board.CardMarked += onCardMarked;
            board.CardRevealed += onCardRevealed;

            GameAction<Board> onStatusChanged = null;
            onStatusChanged = (b) => {
                board.CardMarked -= onCardMarked;
                board.CardRevealed -= onCardRevealed;

                foreach (var item in gameCards) {
                    if (item.Key)
                        Destroy(item.Key.gameObject, 0.5f);
                }
                Destroy(markingCard, 0.5f);
                board.StatusChanged -= onStatusChanged;

                GameStatusChanged(b);
            };
            board.StatusChanged += onStatusChanged; 

            #endregion           

            return gameCards;
        }

        private IEnumerator PlayerTurnRoutine(Dictionary<GameCard, Card> gameCards, Board board) {
            float inputTimeout = 0;
            bool isMark = false;

            while (board.Status == BoardStatus.Active) {
                yield return new WaitForSeconds(_coroutineTimeStep);
                inputTimeout -= Time.deltaTime;

                if (inputTimeout <= 0) {
                    inputTimeout = _axisTimeSensitivity;
                    if (Input.touchCount > 0) {
                        var ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit)) {
                            var gameCard = hit.collider.GetComponent<GameCard>();
                            if (gameCard) {
                                if (isMark)
                                    board.MarkCard(gameCards[gameCard]);
                                else
                                    board.RevealCard(gameCards[gameCard]);
                            }

                            var markingCard = hit.collider.GetComponent<MarkingCard>();
                            if (markingCard) {
                                isMark = !isMark;
                                markingCard.ChangeState(isMark);
                            }
                        }
                    }
                }
            }
        }

        #region Events

        public event GameAction<Board> GameStatusChanged = delegate { };

        #endregion

        #region Manager Methods

        public void CreateBoard(int size, int bombs) {
            var board = new Board(2, size, bombs);
            var gameCards = CreateGameBoard(board);
            StartCoroutine(PlayerTurnRoutine(gameCards, board));
        }

        #endregion
    }
}
