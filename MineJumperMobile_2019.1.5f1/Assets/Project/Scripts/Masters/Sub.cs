using UnityEngine;

namespace MineJumperMobile_2019.Masters {
    [RequireComponent(typeof(Master))]
    public abstract class Sub : MonoBehaviour {

        private Master _master;
        protected Master Master { get { return _master ?? (_master = GetComponent<Master>()); } }
    }
}