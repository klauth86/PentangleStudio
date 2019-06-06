using UnityEngine;

namespace MineJumperMobile_2019.Cards {
    public class MarkingCard : RotatingCard {
        public void ChangeState(bool isRotating) {
            _isRotating = isRotating;
            MeshRenderer.material.color = isRotating ? Color.white : Color.gray;
        }
    }
}
