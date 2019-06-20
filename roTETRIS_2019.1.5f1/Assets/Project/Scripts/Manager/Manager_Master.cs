using roTETRIS.Block;
using UnityEngine;

namespace roTETRIS.Manager {
    public class Manager_Master : MonoBehaviour {
        public event GameEventHandler UpdateScoreEvent;

        public void CallUpdateScoreEvent() {
            UpdateScoreEvent?.Invoke();
        }
    }
}
