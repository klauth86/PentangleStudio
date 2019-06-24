using UnityEngine;

namespace BridgeMaster.Characters.Mob {
    [RequireComponent(typeof(Character_Master))]
    public class AI_Wandering:AI_Base {

        [SerializeField] private bool _isWandering;
        [SerializeField] private Transform[] _pointsOfWandering;

        private void OnEnable() {
        }

        private void OnDisable() {
            
        }
    }
}
