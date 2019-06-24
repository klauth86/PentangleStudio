using UnityEngine;

namespace BridgeMaster.Characters.Mob {
    [RequireComponent(typeof(Master))]
    public class AI_Chasing:MonoBehaviour {

        [SerializeField] private bool _isWandering;
        [SerializeField] private Transform[] _pointsOfWandering;
    }
}
