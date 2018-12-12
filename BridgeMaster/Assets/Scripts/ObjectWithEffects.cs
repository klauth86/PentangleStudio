using UnityEngine;

public class ObjectWithEffects : MonoBehaviour {
    [SerializeField] private GameObject _castVfx;
    [SerializeField] private AudioClip _castSfx;
    [SerializeField] private float _perishAfter;

    protected void PlayEffects() {
        if (_castVfx)
            Instantiate(_castVfx, transform.position, Quaternion.identity);
        if (_castSfx)
            AudioSource.PlayClipAtPoint(_castSfx, Camera.main.transform.position);
        if (_perishAfter > float.Epsilon)
            Destroy(gameObject, _perishAfter);
    }
}
