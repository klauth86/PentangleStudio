namespace HAS {
    public delegate void GameEventHandler();
    public delegate void GameEventHandler<T1>(T1 param1);
    public delegate void GameEventHandler<T1, T2>(T1 param1, T2 param2);
}
