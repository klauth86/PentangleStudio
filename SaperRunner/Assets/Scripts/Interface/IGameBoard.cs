using System.Collections.Generic;

public interface IGameBoard {
    int Dimension { get; }
    ICard[] Cards { get; }

    IEnumerable<ICard> GetNeighbours(ICard card);
}
