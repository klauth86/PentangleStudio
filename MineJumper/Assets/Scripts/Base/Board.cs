using System;
using System.Collections;
using System.Collections.Generic;

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
                Cards[i].OnReveal += OnReveal;
            }

            Shuffle();
            CountIndexes();
        }

        private void OnReveal(Card card) {
            if (card.BombIndex == 0)
                foreach (var item in GetNeighbourCards(card)) {
                    if (!item.IsRevealed)
                        item.Reveal();
                }
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

                foreach (var item in GetNeighbourCards(Cards[i], true)) {
                    if (item.HasBomb)
                        Cards[i].BombIndex++;
                }                
            }            
        }

        private IEnumerable<Card> GetNeighbourCards(Card card, bool withDiagonals = false) {
            var i = Array.IndexOf(Cards, card);
            if (i - 1 >= 0 && (i - 1)/Size == i / Size)
                yield return Cards[i - 1];
            if (i + 1 < BoardSize && (i + 1) / Size == i / Size)
                yield return Cards[i + 1];
            if (i - Size >= 0)
                yield return Cards[i - Size];
            if (i + Size < BoardSize)
                yield return Cards[i + Size];

            if (withDiagonals) {
                if (i - 1 >= 0 && (i - 1) / Size == i / Size && i - 1 - Size >= 0)
                    yield return Cards[i - 1 - Size];
                if (i - 1 >= 0 && (i - 1) / Size == i / Size && i - 1 + Size < BoardSize)
                    yield return Cards[i - 1 + Size];
                if (i + 1 < BoardSize && (i + 1) / Size == i / Size && i + 1 - Size >= 0)
                    yield return Cards[i + 1 - Size];
                if (i + 1 < BoardSize && (i + 1) / Size == i / Size && i + 1 + Size < BoardSize)
                    yield return Cards[i + 1 + Size];
            }
        }
    }
}
