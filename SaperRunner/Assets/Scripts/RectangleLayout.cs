using UnityEngine;

public class RectangleLayout : ILayout {
    private Card _cardPrefab;

    public RectangleLayout(Card cardPrefab) {
        _cardPrefab = cardPrefab;
    }

    public IGameBoard CreateGameBoard(float trumpPercent, int size) {
        var board = new GameBoard();
        var cards = new Card[size * size];
        for (int i = 0; i < size; i++) {
            for (int j = 0; j < size; j++) {
                cards[i * size + j] = GameObject.Instantiate(_cardPrefab);
                cards[i * size + j].transform.position = GetPosition(i, j);
                if (UnityEngine.Random.Range(0.0f, 1.0f) < trumpPercent)
                    cards[i * size + j].IsTrump = true;
            }
        }
        board.Cards = cards;
        return board;
    }

    private Vector3 GetPosition(int i, int j) {
        return new Vector3(i, 0, j);
    }
}
