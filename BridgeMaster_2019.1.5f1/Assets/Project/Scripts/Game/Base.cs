using UnityEngine;

namespace BridgeMaster.Game {
    [RequireComponent(typeof(Master))]
    public abstract class Base : ComponentSubscriber<Master> { }
}
