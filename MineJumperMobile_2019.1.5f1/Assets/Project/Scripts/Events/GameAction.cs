namespace Events {
    public delegate void GameAction();

    public delegate void GameAction<T>(T param);

    public delegate void GameAction<T1, T2>(T1 param1, T2 param2);
}
