using System;

namespace Base {
    public class Card {
        public bool IsMarked;
        public bool IsRevealed;
        public bool HasBomb;
        public int BombIndex;

        public event Action<Card> OnReveal = delegate { };

        public void Reveal() {
            if (IsMarked)
                return;

            IsRevealed = true;
            OnReveal(this);
        }

        public event Action<Card> OnMark = delegate { };

        public void Mark() {
            IsMarked = !IsMarked;
            OnMark(this);
        }
    }
}