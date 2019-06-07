using System;
using System.Collections.Generic;
using System.Linq;

namespace MineJumperMobile_2019.Core {
    public class Board {
        public int Dim;
        public int Size;

        public int Bombs;
        public int Marks;

        public event GameAction<bool> StatusChanged = delegate { };
        public event GameAction<Board, Card> CardRevealed = delegate { };
        public event GameAction<Board, Card> CardMarked = delegate { };

        public int BoardSize { get { return (int)Math.Pow(Size, Dim); } }

        public Card[] Cards;

        public Board(int dim, int size, int bombs) {
            Dim = dim;
            Size = size;
            Bombs = bombs;
            Marks = bombs;

            Cards = new Card[BoardSize];

            for (int i = 0; i < BoardSize; i++) {
                var card = new Card();
                if (i < bombs)
                    card.IsBomb = true;
                Cards[i] = card;
            }

            Shuffle();
            CountIndexes();
        }

        public void RevealCard(Card card) {
            if (card.IsMarked)
                return;

            card.IsRevealed = true;
            CardRevealed(this, card);

            if (card.IsBomb) {
                ChangeBoardStatus(false);
            }

            if (card.BombIndex == 0)
                foreach (var item in GetNeighbourCards(card)) {
                    if (!item.IsRevealed)
                        RevealCard(item);
                }
        }

        public void MarkCard(Card card) {
            card.IsMarked = !card.IsMarked;

            Marks = Marks + (card.IsMarked ? -1 : 1);

            if (Cards.Where(item => item.IsBomb).All(item => item.IsMarked))
                ChangeBoardStatus(true);

            CardMarked(this, card);
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
                if (Cards[i].IsBomb) {
                    Cards[i].BombIndex = 9;
                    continue;
                }

                foreach (var item in GetNeighbourCards(Cards[i], true)) {
                    if (item.IsBomb)
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

        private void ChangeBoardStatus(bool win) {
            StatusChanged(win);
        }
    }
}
