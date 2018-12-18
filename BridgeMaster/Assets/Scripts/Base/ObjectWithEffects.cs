using UnityEngine;

public abstract class ObjectWithEffects : MonoBehaviour {
    [SerializeField] protected GameObject _castVfx;
    [SerializeField] protected AudioClip _castSfx;
    [SerializeField] protected float _perishAfter;

    protected void PlayEffects() {
        if (_castVfx)
            Instantiate(_castVfx, transform.position, Quaternion.identity);
        if (_castSfx)
            AudioSource.PlayClipAtPoint(_castSfx, Camera.main.transform.position);
        if (_perishAfter < float.PositiveInfinity)
            Destroy(gameObject, _perishAfter);
    }
}
