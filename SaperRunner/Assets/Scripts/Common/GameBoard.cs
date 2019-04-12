using System.Collections.Generic;
using UnityEngine;

public class GameBoard : IGameBoard {
    public int Dimension { get; set; }
    public int Size { get; set; }
    public ICard[] Cards { get; set; }

    public IEnumerable<ICard> GetNeighbours(ICard card) {
        var index = -1;
        for (int i = 0; i < Size; i++) {
            if (Cards[i] == card) {
                index = i;
                break;
            }
        }
        if (index >= 0)
            for (int i = 0; i < Size; i++) {
                if (Mathf.Abs((i - index) % Size) <= 1) {
                    yield return Cards[i];
                }
            }
    }
}
