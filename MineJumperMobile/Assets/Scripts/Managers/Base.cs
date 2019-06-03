﻿using UnityEngine;

namespace Managers {
    [RequireComponent(typeof(Master))]
    public class Base : MonoBehaviour {
        private Master _master;

        // Use this for initialization
        void Start() {
            _master = GetComponent<Master>();
        }
    }
}
