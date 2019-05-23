using Base;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class GameCard : MonoBehaviour {
    [SerializeField] private Material _unrevealedMaterial;
    [SerializeField] private Material _markedMaterial;

    [SerializeField] private Material[] _indexMaterials;

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

        MeshRenderer.material = _indexMaterials[card.BombIndex];

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
        MeshRenderer.material = card.IsMarked ? _markedMaterial : (card.IsRevealed ? _indexMaterials[card.BombIndex] : _unrevealedMaterial);
    }

    private void OnReveal(Card card) {
        MeshRenderer.material = _indexMaterials[card.BombIndex];
    }
}
