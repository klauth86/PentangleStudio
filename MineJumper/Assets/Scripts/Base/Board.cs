using System;

namespace Base {
    public class Board {
        public int Dim;
        public int Size;
        public int Bombs;

        public int BoardSize { get { return (int)Math.Pow(Size, Dim); } }

        public Card[] Cards;

        public Board(int dim, int size, int bombs) {
            Dim = dim;
            Size = size;
            Bombs = bombs;

            Cards = new Card[BoardSize];

            for (int i = 0; i < BoardSize; i++) {
                var card = new Card();
                if (i < bombs)
                    card.HasBomb = true;
                Cards[i] = card;
            }

            Shuffle();
            CountIndexes();
        }

        private void Shuffle() {
            var rnd = new Random();
            for (int i = 0; i < BoardSize; i++) {
                var index = rnd.Next(0, BoardSize);

                var tmp = Cards[index];
                Cards[index] = Cards[i];
                Cards[i] = tmp;
            }
        }

        private void CountIndexes() {
            for (int i = 0; i < BoardSize; i++) {
                if (Cards[i].HasBomb) {
                    Cards[i].BombIndex = 9;
                    continue;
                }

                if (i - 1 >= 0 && Cards[i - 1].HasBomb)
                    Cards[i].BombIndex++;
                if (i + 1 < BoardSize && Cards[i + 1].HasBomb)
                    Cards[i].BombIndex++;

                if (i - Size >= 0 && Cards[i - Size].HasBomb)
                    Cards[i].BombIndex++;
                if (i + Size < BoardSize && Cards[i + Size].HasBomb)
                    Cards[i].BombIndex++;

                if (i - 1 >= 0 && i - 1 - Size >= 0 && Cards[i - 1 - Size].HasBomb)
                    Cards[i].BombIndex++;
                if (i - 1 >= 0 && i - 1 + Size < BoardSize && Cards[i - 1 + Size].HasBomb)
                    Cards[i].BombIndex++;

                if (i + 1 < BoardSize && i + 1 - Size >= 0 && Cards[i + 1 - Size].HasBomb)
                    Cards[i].BombIndex++;
                if (i + 1 < BoardSize && i + 1 + Size < BoardSize && Cards[i + 1 + Size].HasBomb)
                    Cards[i].BombIndex++;
            }
        }
    }
}
