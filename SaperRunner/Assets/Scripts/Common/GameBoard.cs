using System.Collections.Generic;

public class GameBoard : IGameBoard {
    public int Dimension { get; set; }
    public ICard[] Cards { get; set; }

    public IEnumerable<ICard> GetNeighbours(ICard card) {
        throw new System.NotImplementedException();
    }
}
