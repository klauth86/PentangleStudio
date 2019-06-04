using Events;

namespace Core {
    public class Card {
        public bool HasBomb;
        public int BombIndex;

        public event GameAction<Card> OnMark = delegate { };
        private bool _isMarked;
        public bool IsMarked {
            get {
                return _isMarked;
            }
            set {
                _isMarked = value;
                OnMark(this);
            }
        }


        public event GameAction<Card> OnReveal = delegate { };
        private bool isRevealed;
        public bool IsRevealed {
            get {
                return isRevealed;
            }

            set {
                isRevealed = value;
                OnReveal(this);
            }
        }
    }
}