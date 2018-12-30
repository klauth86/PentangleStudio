public class IGameMaster {
    public GameStatus Status { get; set; }
    public IGameBoard Board { get; set; }

    public IGameMaster() {
        Status = GameStatus.NoActiveGame;
    }

    public void StartGame(float trumpPercent, int size, ILayout layout) {
        Board = layout.CreateGameBoard(trumpPercent, size);
        Status = GameStatus.IsActive;
    }

    public void FlipCard(ICard card) {
        if (card.IsMarked)
            return;

        card.Flip();
        if (card.IsTrump) {
            Status = GameStatus.GameOver;
        }
        else if (card.Index == 0)
            FlipNeighbours(card);
    }

    public void MarkCard(ICard card) {
        card.IsMarked = !card.IsMarked;
    }

    private void FlipNeighbours(ICard card) {
        foreach (var neighbourCard in Board.GetNeighbours(card)) {
            neighbourCard.Flip();
        };
    }
}
