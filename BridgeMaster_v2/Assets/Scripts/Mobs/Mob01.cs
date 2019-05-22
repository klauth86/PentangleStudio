using Base;
using UnityEngine;

public class Mob01 : MobBase {
    public void Attack() {
        var playerCollider = _player.GetComponent<Collider2D>();
        var mobCollider = GetComponent<BoxCollider2D>();
        if (mobCollider.IsTouching(playerCollider))
            _player.TakeDamage(_attackDamage);
    }
}
