﻿using BridgeMaster.Base;
using UnityEngine;

namespace BridgeMaster.Characters.AI {
    public class AI_Attack : COM_Handler<AI> {

        [SerializeField] private float _damage = 30;       

        public void Attack() {
            var target = Master.Target.GetComponent<Character_Master>();
            if (target) {
                target.ChangeHealth(-_damage);
            }
        }
    }
}