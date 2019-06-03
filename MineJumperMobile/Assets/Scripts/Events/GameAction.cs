namespace Events {

    public delegate void GameAction(object sender);

    public delegate void GameAction<T>(object sender, T param);

    public delegate void GameAction<T1, T2>(object sender, T1 param1, T2 param2);
}
