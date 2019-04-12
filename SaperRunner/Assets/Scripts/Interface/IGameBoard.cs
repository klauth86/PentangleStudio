using System.Collections.Generic;

public interface IGameBoard {
    int Dimension { get; set; }
    ICard[] Cards { get; set; }
    IEnumerable<ICard> GetNeighbours(ICard card);
}
