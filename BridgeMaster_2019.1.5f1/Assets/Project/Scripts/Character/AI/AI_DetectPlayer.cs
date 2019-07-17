﻿using BridgeMaster.Base;
using UnityEngine;

namespace BridgeMaster.Characters.AI {
    public class AI_DetectPlayer : COM_Subscriber<AI> {
        [SerializeField] private float _detectRange = 3;

        private void Update() {
            var playerTransform = Player.Player.Instance.MyTransform;
            var canBeDetected = Vector3.Distance(playerTransform.position, Master.MyTransform.position) < _detectRange;

            if (Master.Target != playerTransform && canBeDetected) {
                Master.SetTarget(playerTransform);
            }

            if (Master.Target == playerTransform && !canBeDetected)
                Master.SetTarget(null);
        }
    }
}