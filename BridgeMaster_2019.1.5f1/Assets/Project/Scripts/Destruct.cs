using System.Collections;
using UnityEngine;

public class Destruct : MonoBehaviour
{
    [SerializeField] private Rigidbody2D[] _rbs;
    [SerializeField] private bool _destruct;
    [SerializeField] private float _destructionRate=0.1f;

    private void Update() {
        if (_destruct && _rbs != null) {
            _destruct = false;
            StartCoroutine(DestructRoutine(Random.Range(0, _rbs.Length)));
        }
    }

    private IEnumerator DestructRoutine(int startIndex) {
        int i = 1;
        _rbs[startIndex].isKinematic = false;

        while (i < _rbs.Length) {
            yield return new WaitForSeconds(_destructionRate);
            if (startIndex - i >= 0)
                _rbs[startIndex - i].isKinematic = false;

            if (startIndex + i < _rbs.Length)
                _rbs[startIndex + i].isKinematic = false;

            i++;
        }
    }
}
