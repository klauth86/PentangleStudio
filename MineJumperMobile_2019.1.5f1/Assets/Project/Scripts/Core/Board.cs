using System;
using System.Collections.Generic;
using System.Linq;

namespace MineJumperMobile_2019.Core {
    public class Board {
        public event GameAction<bool> StatusChanged = delegate { };
        public event GameAction<Board, Card> CardRevealed = delegate { };
        public event GameAction<Board, Card> CardMarked = delegate { };

        private int _size;

        public Card[] Cards;

        public Board(int size , int bombs) {
            _size = size ;

            Cards = new Card[size*_size ];

            for (int i = 0; i < _size * _size; i++) {
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

            CardMarked(this, card);

            if (Cards.Where(item => item.IsBomb).All(item => item.IsMarked))
                ChangeBoardStatus(true);
        }

        private void Shuffle() {
            var rnd = new Random();
            for (int i = 0; i < _size * _size; i++) {
                var index = rnd.Next(0, _size * _size);

                var tmp = Cards[index];
                Cards[index] = Cards[i];
                Cards[i] = tmp;
            }
        }

        private void CountIndexes() {
            for (int i = 0; i < _size * _size; i++) {
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
            if (i - 1 >= 0 && (i - 1)/_size == i /_size )
                yield return Cards[i - 1];
            if (i + 1 < _size * _size && (i + 1) /_size  == i /_size )
                yield return Cards[i + 1];
            if (i -_size  >= 0)
                yield return Cards[i -_size ];
            if (i +_size  < _size * _size)
                yield return Cards[i +_size ];

            if (withDiagonals) {
                if (i - 1 >= 0 && (i - 1) /_size  == i /_size  && i - 1 -_size  >= 0)
                    yield return Cards[i - 1 -_size ];
                if (i - 1 >= 0 && (i - 1) /_size  == i /_size  && i - 1 +_size  < _size * _size)
                    yield return Cards[i - 1 +_size ];
                if (i + 1 < _size * _size && (i + 1) /_size  == i /_size  && i + 1 -_size  >= 0)
                    yield return Cards[i + 1 -_size ];
                if (i + 1 < _size * _size && (i + 1) /_size  == i /_size  && i + 1 +_size  < _size * _size)
                    yield return Cards[i + 1 +_size ];
            }
        }

        private void ChangeBoardStatus(bool win) {
            StatusChanged(win);
        }
    }
}
