using Base;
using UnityEngine;

public class Game : MonoBehaviour {

    [SerializeField] private GameObject _gameCardPrefab;

    [SerializeField] private int _size;
    [SerializeField] private int _bombs;

    // Use this for initialization
    void Start() {
        var board = new Board(2, _size, _bombs);
        CreateBoard(board);
        AdjustCamera(board);
    }

    void CreateBoard(Board board) {
        var offset = board.Size % 2 == 0 ? 0.5f : 0.0f;
        for (int i = 0; i < board.BoardSize; i++) {
            var gameCard = Instantiate(_gameCardPrefab, new Vector3(i % board.Size - board.Size / 2 + offset, 0, i / board.Size - board.Size / 2 + offset), Quaternion.identity, transform).GetComponent<GameCard>();
            gameCard.Card = board.Cards[i];
        }
    }

    private void AdjustCamera(Board board) {
        Camera.main.transform.position = new Vector3(0, (board.Size / 2 + 1) /Mathf.Tan(Mathf.PI/6), 0);
    }
}
