using System;
using Base;
using Dicts;
using UnityEngine;

public class Mob01 : MobBase {
    public void Attack() {
        OnFixedUpdate += MakeAnAttack;
    }

    private void MakeAnAttack() {
        var playerCollider = _player.GetComponent<Collider2D>();
        var mobCollider = GetComponent<BoxCollider2D>();
        if (mobCollider.IsTouching(playerCollider)) {
            if (!_player.TakeDamage(_attackDamage)) {
                _isAttacking = false;
                _animator.SetBool(AnimatorKey.IsAttacking, false);
                _animator.SetBool(AnimatorKey.IsWalking, false);
                Target = null;
            }
        }

        OnFixedUpdate -= MakeAnAttack;
    }
}
