using Assets.Project.Scripts.Dicts;
using System;
using System.Collections;
using UnityEngine;

namespace BridgeMaster.Characters.AI {
    public class AI_Controller : Base<AI> {

        private void OnEnable() {
            Master.ToggleFreezeEvent += ToggleFreeze;
        }

        private void OnDisable() {
            Master.ToggleFreezeEvent -= ToggleFreeze;
        }

        private void ToggleFreeze() {
            Master.Rigidbody.constraints = Master.IsFreezed ? RigidbodyConstraints2D.FreezeAll : RigidbodyConstraints2D.FreezeRotation;
        }
    }
}