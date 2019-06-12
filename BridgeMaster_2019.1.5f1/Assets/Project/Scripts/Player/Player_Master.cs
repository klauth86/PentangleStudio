using BridgeMaster;
using UnityEngine;

namespace BridgeMaster.Player {
    public class Player_Master : MonoBehaviour {
        public event GameEventHandler<float> HealthChangedEvent;
        public event GameEventHandler<float> ManaChangedEvent;
        public event GameEventHandler<float> IronChangedEvent;
    }
}