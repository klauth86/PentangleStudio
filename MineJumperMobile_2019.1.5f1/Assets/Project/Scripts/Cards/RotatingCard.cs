﻿using UnityEngine;
using Random = UnityEngine.Random;

namespace MineJumperMobile_2019.Cards {
    [RequireComponent(typeof(MeshRenderer))]
    public class RotatingCard : MonoBehaviour {
        public static int Counter;

        public int Id;

        public RotatingCard() {
            Id = Counter++;
        }


        [SerializeField] protected bool _isRotating;
        [SerializeField] private float _rotationVelocity;

        protected Vector3 _rotationVector;

        private MeshRenderer _meshRenderer;
        protected MeshRenderer MeshRenderer {
            get {
                return _meshRenderer ?? (_meshRenderer = GetComponent<MeshRenderer>());
            }
        }

        // Use this for initialization
        void Start() {
            _rotationVector = new Vector3(Random.Range(0.0f, 1.0f),
            Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f)).normalized;
        }

        // Update is called once per frame
        void Update() {
            if (_isRotating)
                transform.Rotate(_rotationVector * Time.deltaTime * _rotationVelocity);
        }
    }
}
