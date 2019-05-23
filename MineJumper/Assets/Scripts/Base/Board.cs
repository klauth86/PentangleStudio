using System;

namespace Base {
    public class Board {
        public int Dim;
        public int Size;

        public int BoardSize { get { return (int)Math.Pow(Size, Dim); } }

        public Card[] Cards;

        public Board(int dim, int size, int bombs) {
            Cards = new Card[BoardSize];

            for (int i = 0; i < BoardSize; i++) {
                var card = new Card();
                if (i < bombs)
                    card.HasBomb = true;
            }

            Shuffle();
        }

        private void Shuffle() {
            throw new NotImplementedException();
        }
    }
}
