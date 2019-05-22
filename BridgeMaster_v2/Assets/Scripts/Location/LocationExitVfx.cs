using Base;
using Dicts;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocationExitVfx : MonoBehaviour {

    [SerializeField] private float _rotation;
    [SerializeField] private float _focusDurationTime;

    [SerializeField] private float _targetScale;
    [SerializeField] private int _iterationsCount;

    public void LoadLocation(Location nextLocation) {
        foreach (var character in FindObjectsOfType<CharacterBase>()) {
            character.PauseCharacter();
        }
        StartCoroutine(FocusOnPlayerRoutine(nextLocation));
    }

    private IEnumerator FocusOnPlayerRoutine(Location nextLocation) {
        var delta = (transform.localScale.x - _targetScale) / _iterationsCount;
        while (transform.localScale.x > _targetScale) {
            transform.localScale = new Vector3(transform.localScale.x - delta,
                transform.localScale.y - delta, transform.localScale.z);
            transform.Rotate(Vector3.negativeInfinity, _rotation / _iterationsCount);
            yield return new WaitForSeconds(_focusDurationTime / _iterationsCount);
        }
        SceneManager.LoadScene(nextLocation.ToString());
    }
}