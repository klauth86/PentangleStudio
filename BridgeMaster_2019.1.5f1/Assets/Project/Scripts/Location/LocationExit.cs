using Dicts;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LocationExit : MonoBehaviour {
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private GameObject _locationExitVfx;
    [SerializeField] private Location _nextLocation;

    private Transform _transform;

    private void Start() {
        StartCoroutine(CheckIfPlayerIsInLocationExit());
        _transform = transform;
    }

    private IEnumerator CheckIfPlayerIsInLocationExit() {
        while(isActiveAndEnabled) {
            var hit = Physics2D.CapsuleCast(new Vector2(_transform.position.x, _transform.position.y), new Vector2(2, 7), CapsuleDirection2D.Vertical, 0, Vector2.zero, 0, _playerLayer);
            if (hit) {
                if (_locationExitVfx) {
                    Instantiate(_locationExitVfx);
                }
            }
            yield return new WaitForSeconds(0.0625f);
        }
    }
}
