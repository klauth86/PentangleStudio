﻿namespace roTETRIS {
    public delegate void GameEventHandler();
    public delegate void GameEventHandler<T1>(T1 param1);
    public delegate void GameEventHandler<T1, T2>(T1 param1, T2 param2);
    public delegate void GameEventHandler<T1, T2, T3>(T1 param1, T2 param2, T3 param3);
}