namespace BridgeMaster.Base {

    public delegate void EventHandler();

    public delegate void EventHandler<T>(T param);

    public delegate void EventHandler<T1, T2>(T1 param1, T2 param2);

    public delegate void EventHandler<T1, T2, T3>(T1 param1, T2 param2, T3 param3);
}
