namespace BridgeMaster.Base {

    public delegate TRes Functor<TRes>();

    public delegate TRes Functor<TRes, T>(T param);

    public delegate TRes Functor<TRes, T1, T2>(T1 param1, T2 param2);

    public delegate TRes Functor<TRes, T1, T2, T3>(T1 param1, T2 param2, T3 param3);
}
