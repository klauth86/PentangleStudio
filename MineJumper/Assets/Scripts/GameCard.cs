using Base;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer)]
public class GameCard : MonoBehaviour {

    [SerializeField] private Material[] _materials;

    private MeshRenderer _meshRenderer;

    private Card card;

    public Card Card {
        get {
            return card;
        }

        set {
            Unsubscribe(card);
            card = value;
            InitAndSubscribe(card);
        }
    }

    private void InitAndSubscribe(Card card) {
        if (card == null)
            return;

        var mr = _meshRenderer ?? (_meshRenderer = GetComponent<MeshRenderer>());
        mr.material = _materials[card.BombIndex];

        card.OnMark += OnMark;
        card.OnReveal += OnReveal;
    }

    private void Unsubscribe(Card card) {
        if (card == null)
            return;

        card.OnMark -= OnMark;
        card.OnReveal -= OnReveal;
    }

    private void OnMark() {        
    }

    private void OnReveal() {
    }
}
