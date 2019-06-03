﻿using Base;
using System.Collections;
using UnityEngine;

namespace Cards {
    [RequireComponent(typeof(Rigidbody))]
    public class GameCard : RotatingCard {
        [SerializeField] private Material _unrevealedMaterial;
        [SerializeField] private Material _markedMaterial;
        [SerializeField] private Material[] _indexMaterials;

        [SerializeField] private float _coroutineTimeStep;
        [SerializeField] private float _collapseTime;

        [SerializeField] public GameObject SelectionObject;

        private Rigidbody _rigidbody;
        private Rigidbody Rigidbody {
            get {
                return _rigidbody ?? (_rigidbody = GetComponent<Rigidbody>());
            }
        }

        public GameCard up { get; set; }
        public GameCard right { get; set; }
        public GameCard down { get; set; }
        public GameCard left { get; set; }

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
            MeshRenderer.material = card.IsMarked ? _markedMaterial : _unrevealedMaterial;
        }

        private void OnReveal(Card card) {
            MeshRenderer.material = _indexMaterials[card.BombIndex];
            if (card.BombIndex == 0) {
                if (up != null) {
                    up.down = down;
                }
                if (down != null) {
                    down.up = up;
                }
                if (left != null) {
                    left.right = right;
                }
                if (right != null) {
                    right.left = left;
                }
                StartCoroutine(CollapseRoutine());
            }
        }

        private IEnumerator CollapseRoutine() {
            LevelManager.Instance.AudioManager.PlayCollapseClip();
            var n = _collapseTime / _coroutineTimeStep;
            for (int i = 1; i <= n; i++) {
                transform.localScale = (transform.localScale * (n - i)) / n;
                yield return new WaitForSeconds(_coroutineTimeStep);
            }
            Destroy(gameObject);
        }

        private void OnMouseOver() {
            if (InputDevice.Mouse != LevelManager.Instance.InputDevice)
                return;

            if (Input.GetMouseButtonDown(0)) {
                card.Reveal();
            }

            if (Input.GetMouseButtonDown(1)) {
                card.Mark(LevelManager.Instance.BombsLeft);
            }
        }

        private void OnDisable() {
            Card = null;
        }
    }
}