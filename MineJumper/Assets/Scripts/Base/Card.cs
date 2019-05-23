using System;

namespace Base {
    public class Card {
        public bool IsMarked;
        public bool IsRevealed;
        public bool HasBomb;
        public int BombIndex;

        public event Action<Card> OnReveal = delegate { };

        public void Reveal() {
            OnReveal(this);
        }

        public event Action<Card> OnMark = delegate { };

        public void Mark() {
            OnMark(this);
        }
    }
}