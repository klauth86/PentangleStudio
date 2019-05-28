using Base;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]
public class GameCard : MonoBehaviour {
    [SerializeField] private Material _unrevealedMaterial;
    [SerializeField] private Material _markedMaterial;
    [SerializeField] private Material[] _indexMaterials;

    [SerializeField] private float _rotationVelocity;
    [SerializeField] private float _stiffnessKoefficient;

    [SerializeField] private GameObject _selectionObject; 


    public static GameCard SelectedCard;
    public bool IsSelected {
        set {
            SelectedCard = value ? this : null;
            _selectionObject.SetActive(value);
            OnSelectionChanged(this, value);
        }
    }

    public event Action<GameCard, bool> OnSelectionChanged = delegate { };

    private MeshRenderer _meshRenderer;
    private MeshRenderer MeshRenderer {
        get {
            return _meshRenderer ?? (_meshRenderer = GetComponent<MeshRenderer>());
        }
    }

    private Rigidbody _rigidbody;
    private Rigidbody Rigidbody {
        get {
            return _rigidbody ?? (_rigidbody = GetComponent<Rigidbody>());
        }
    }

    private Vector3 _rotationVector;
    private Vector3 _initPosition;

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

        MeshRenderer.material = _unrevealedMaterial;

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

    private void Start() {
        _rotationVector = new Vector3(
                Random.Range(0.0f, 1.0f),
                Random.Range(0.0f, 1.0f),
                Random.Range(0.0f, 1.0f)).normalized;
        _initPosition = transform.position;
    }

    private void Update() {
        transform.Rotate(_rotationVector * Time.deltaTime * _rotationVelocity);
    }

    private void FixedUpdate() {
        Rigidbody.AddForce(-_stiffnessKoefficient * (transform.position - _initPosition));
    }

    private void OnMouseDown() {
        if (SelectedCard != this) {
            SelectedCard.IsSelected = false;
            IsSelected = true;
        }
    }
}
