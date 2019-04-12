using System.Collections;
using UnityEngine;

public class Spell : ObjectWithEffects {
    [SerializeField] protected ResourceUnit _resource;
    [SerializeField] protected Bridge _startBridge;
    [SerializeField] protected Bridge _middleBridge;
    [SerializeField] protected Bridge _endBridge;
    [SerializeField] protected float _castDuration;

    [SerializeField] protected bool _isCasting;

    protected Coroutine _castingCoroutine;

    public void StartCast(Vector2 from, Vector2 to) {
        if (!_isCasting) {
            Debug.Log("StartCast");
            _isCasting = true;
            _castingCoroutine = StartCoroutine(CastingCoroutine(from, to));
        }
    }

    private IEnumerator CastingCoroutine(Vector2 from, Vector2 to) {
        var n = (to.x - from.x) / _startBridge.Offset.x;
        CreateSegment(from, _startBridge);
        var currentPos = from + new Vector2(_startBridge.Offset.x, 0);
        for (int i = 0; i < n; i++) {
            currentPos = currentPos + new Vector2(_startBridge.Offset.x, 0);
            CreateSegment(currentPos, _middleBridge);
            yield return new WaitForSeconds(_castDuration);
        }
    }

    private void CreateSegment(Vector2 currentPos, Bridge currentBridge) {
        Instantiate(currentBridge.gameObject, currentPos, Quaternion.identity);
    }

    public void StopCast() {
        if (_isCasting) {
            Debug.Log("Stop casting");
            _isCasting = false;
            if (_castingCoroutine != null)
            StopCoroutine(_castingCoroutine);
        }
    }
}
