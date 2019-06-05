using System.Collections.Generic;
using Base;
using BridgeMaster.Dicts;
using UnityEngine;

public class MobGroup : MonoBehaviour {

    [Header(Header.Mob)]
    [SerializeField] private GameObject _mobPrefab;

    [Header(Header.Movement)]
    [SerializeField] private bool _isHunting;
    [SerializeField] private List<Transform> _points;


    private MobBase _mob;

    // Use this for initialization
    void Start() {
        _mob = Instantiate(_mobPrefab, transform.position, Quaternion.identity, transform).GetComponent<MobBase>();
        if (_points.Count > 0) {
            _mob.Target = GetNextPoint();
        }
    }

    // Update is called once per frame
    void Update() {
        if (_points.Contains(_mob.Target) && Vector3.Distance(_mob.transform.position, _mob.Target.position) < 0.2) {
            _mob.Target = GetNextPoint();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (_isHunting && collision.gameObject.GetComponent<Player>()) {
            _mob.Target = collision.gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.GetComponent<Player>() && _mob.Target == collision.gameObject.transform) {
            _mob.Target = GetNextPoint();
        }
    }

    private Transform GetNextPoint() {
        if (!_mob.Target || !_points.Contains(_mob.Target))
            return _points[0];
        var targetIndex = _points.IndexOf(_mob.Target);
        return _points[(targetIndex + 1) % _points.Count];
    }
}
