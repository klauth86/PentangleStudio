using System;

namespace Base {
    public class Card {
        public bool IsMarked;
        public bool IsRevealed;
        public bool HasBomb;
        public int BombIndex;

        public event Action OnReveal = delegate { };

        public void Reveal() {
            OnReveal();
        }

        public event Action OnMark = delegate { };

        public void Mark() {
            OnMark();
        }
    }
}