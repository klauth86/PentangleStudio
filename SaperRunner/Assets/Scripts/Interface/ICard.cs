public interface ICard {
    bool IsMarked { get; set; }
    bool IsTrump { get; set; }
    int Index { get; set; }

    void Flip();
}