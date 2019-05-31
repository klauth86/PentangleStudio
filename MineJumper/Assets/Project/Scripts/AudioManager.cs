using System;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    [SerializeField] private AudioClip _buttonClip;

    public void PlayButtonAudio() {
        PlayAudio(_buttonClip);
    }

    private void PlayAudio(AudioClip clip) {
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
    }
}
