using MineJumperMobile_2019.Cards;
using MineJumperMobile_2019.Core;
using MineJumperMobile_2019.Dicts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MineJumperMobile_2019.Masters {
    public class SubBoard : Sub {

        private bool _isMark;
        private Board _board;
        private Dictionary<GameCard, Card> _gameCards;
        private MarkingCard _markingCard;

        #region Inspector

        [SerializeField] private float _touchDeadZoneDuration;
        [SerializeField] private GameObject _markingCardPrefab;
        [SerializeField] private GameObject _gameCardPrefab;
        [SerializeField] private float _scaleFactor;

        private int _bombsLeft;
        private float _touchDeadZone;

        #endregion

        private void CreateBoard(int size, int bombs) {
            _board = new Board(2, size, bombs);
            _gameCards = CreateGameBoard(_board);
            Master.CallBombsLeftChangedEvent(bombs);
        }

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

            _markingCard = Instantiate(_markingCardPrefab, new Vector3(-3, cameraOffset - 4, 0), Quaternion.identity).GetComponent<MarkingCard>();

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

            GameAction<bool> onStatusChanged = null;
            onStatusChanged = (b) => {
                board.CardMarked -= onCardMarked;
                board.CardRevealed -= onCardRevealed;
                board.StatusChanged -= onStatusChanged;

                Master.CallGameOverEvent(b);
            };
            board.StatusChanged += onStatusChanged;

            #endregion

            return gameCards;
        }

        private void Clear() {
            if (_gameCards != null)
            foreach (var item in _gameCards) {
                if (item.Key)
                    Destroy(item.Key.gameObject);
            }
            if (_markingCard)
            Destroy(_markingCard.gameObject);

            _isMark = false;
            _board = null;
        }

        private void Update() {
            _touchDeadZone -= Time.deltaTime;
            if (Input.touchCount > 0 && Time.timeScale > 0 && _touchDeadZone <= 0) {
                _touchDeadZone = _touchDeadZoneDuration;
                var ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit)) {
                    var gameCard = hit.collider.GetComponent<GameCard>();
                    if (gameCard) {
                        if (_isMark) {
                            var bombsLeft = _board.Bombs - _board.Cards.Count(card => card.IsMarked);
                            if (bombsLeft > 0 || _gameCards[gameCard].IsMarked) {
                                _board.MarkCard(_gameCards[gameCard]);
                                Master.CallBombsLeftChangedEvent(_board.Bombs - _board.Cards.Count(card => card.IsMarked));
                            }
                        }                            
                        else
                            _board.RevealCard(_gameCards[gameCard]);
                    }

                    var markingCard = hit.collider.GetComponent<MarkingCard>();
                    if (markingCard) {
                        _isMark = !_isMark;
                        markingCard.ChangeState(_isMark);
                    }
                }
            }
        }

        private void OnEnable() {
            Master.ButtonActionEvent += OnButtonActionEvent;
        }

        private void OnDisable() {
            Master.ButtonActionEvent -= OnButtonActionEvent;
        }

        private void OnButtonActionEvent(ButtonAction param) {
            if (param == ButtonAction.PlayButtonAction) {
                Clear();
                CreateBoard(Master.Size, Master.Bombs);
            }
            if (param == ButtonAction.MenuButtonAction) {
                Clear();
            }
        }
    }
}