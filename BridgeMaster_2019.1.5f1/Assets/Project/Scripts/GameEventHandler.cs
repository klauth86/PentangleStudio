namespace BridgeMaster {
    public delegate void GameEventHandler();
    public delegate void GameEventHandler<T>(T param);
    public delegate void GameEventHandler
        <T1, T2>(T1 param1, T2 param2);
}
