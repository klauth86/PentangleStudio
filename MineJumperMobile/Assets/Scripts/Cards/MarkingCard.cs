using UnityEngine;

namespace Cards {
    public class MarkingCard : RotatingCard {
        public void ChangeState(bool isMarking) {
            _isRotating = isMarking;
            MeshRenderer.material.color = isMarking ? Color.white : Color.gray;
        }
    }
}
