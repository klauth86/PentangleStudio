using MineJumperMobile_2019.Core;
using MineJumperMobile_2019.Dicts;
using UnityEngine;

namespace MineJumperMobile_2019.Masters {
    public class Master : MonoBehaviour {
        public event GameAction<ButtonAction> ButtonActionEvent;
        public event GameAction<bool> GameOverEvent;
        public event GameAction<int> BombsLeftChangedEvent;

        public void CallButtonActionEvent(ButtonAction buttonAction) {
            ButtonActionEvent?.Invoke(buttonAction);
        }

        public void CallGameOverEvent(bool win) {
            GameOverEvent?.Invoke(win);
        }

        public void CallBombsLeftChangedEvent(int bombsLeft) {
            BombsLeftChangedEvent?.Invoke(bombsLeft);
        }
    }
}