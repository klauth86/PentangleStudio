using UnityEngine;

public class Card : MonoBehaviour, ICard {
    [SerializeField] Animator _animator;

    public bool IsMarked { get; set; }
    public bool IsTrump { get; set; }
    public int Index { get; set; }

    public void Flip() {
        if (IsTrump && _animator)
            _animator.SetTrigger("Explosion");
    }
}
