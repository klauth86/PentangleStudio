using System;

namespace Base {
    public class Card {
        public bool IsMarked;
        public bool IsRevealed;
        public bool HasBomb;
        public int BombIndex;

        public event Action<Card> OnReveal = delegate { };

        public bool Reveal() {
            if (IsMarked)
                return false;

            IsRevealed = true;
            OnReveal(this);
            return BombIndex == 0;
        }

        public event Action<Card> OnMark = delegate { };

        public void Mark(int left) {
            if (!IsMarked && left <= 0)
                return;
            IsMarked = !IsMarked;
            OnMark(this);
        }
    }
}