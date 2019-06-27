using UnityEngine;

namespace BridgeMaster.Characters {
    class Character_Health : Base<Master> {
        [SerializeField] private float _max;
        [SerializeField] private float _health;

        private void OnEnable() {
            Master.ChangeHealthEvent += ChangeHealth;
        }

        private void OnDisable() {
            Master.ChangeHealthEvent -= ChangeHealth;
        }

        private void ChangeHealth(float amount) {
            _health += amount;
            if (_health > _max)
                _health = _max;

            if (_health < 0) {
                _health = 0;
                Master.Die();
            }
        }
    }
}