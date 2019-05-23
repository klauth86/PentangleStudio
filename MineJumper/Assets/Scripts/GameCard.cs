using Base;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class GameCard : MonoBehaviour {

    [SerializeField] private Material[] _materials;
    [SerializeField] private Material _markedMaterial;

    private MeshRenderer _meshRenderer;
    private MeshRenderer MeshRenderer {
        get {
            return _meshRenderer ?? (_meshRenderer = GetComponent<MeshRenderer>());
        }
    }

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

        MeshRenderer.material = _materials[card.BombIndex];

        card.OnMark += OnMark;
        card.OnReveal += OnReveal;
    }

    private void Unsubscribe(Card card) {
        if (card == null)
            return;

        card.OnMark -= OnMark;
        card.OnReveal -= OnReveal;
    }

    private void OnMark(Card card) {
        MeshRenderer.material = card.IsMarked ? _markedMaterial : _materials[card.BombIndex];
    }

    private void OnReveal(Card card) {

    }
}
