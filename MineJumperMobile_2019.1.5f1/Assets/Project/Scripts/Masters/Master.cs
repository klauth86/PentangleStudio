using MineJumperMobile_2019.Cards;
using MineJumperMobile_2019.Core;
using MineJumperMobile_2019.Dicts;
using UnityEngine;

namespace MineJumperMobile_2019.Masters {
    public class Master : MonoBehaviour {

        public static Master Instance;

        public Master() {
            Instance = this;
        }
        public event GameAction<MarkingCard> MarkingCardMouseClickedEvent;
        public event GameAction<GameCard> GameCardMouseClickedEvent;
        public event GameAction<ButtonAction> ButtonActionEvent;
        public event GameAction<bool> GameOverEvent;
        public event GameAction<int> BombsLeftChangedEvent;

        public event GameAction<int> SizeChangedEvent;
        public event GameAction<int> BombsChangedEvent;

        public int Size;
        public int Bombs;

        public void CallMarkingCardMouseClickedEvent(MarkingCard card) {
            MarkingCardMouseClickedEvent?.Invoke(card);
        }

        public void CallGameCardMouseClickedEvent(GameCard card) {
            GameCardMouseClickedEvent?.Invoke(card);
        }

        public void CallButtonActionEvent(ButtonAction buttonAction) {
            ButtonActionEvent?.Invoke(buttonAction);
        }

        public void CallGameOverEvent(bool win) {
            GameOverEvent?.Invoke(win);
        }

        public void CallBombsLeftChangedEvent(int bombsLeft) {
            BombsLeftChangedEvent?.Invoke(bombsLeft);
        }

        public void CallSizeChangedEvent(int size) {
            Size = size;
            SizeChangedEvent?.Invoke(size);
        }

        public void CallBombsChangedEvent(int bombs) {
            Bombs = bombs;
            BombsChangedEvent?.Invoke(bombs);
        }
    }
}