using UnityEngine;

public class AudioManager : MonoBehaviour {

    [SerializeField] private AudioClip _buttonClip;
    [SerializeField] private AudioClip _winClip;
    [SerializeField] private AudioClip _loseClip;
    [SerializeField] private AudioClip _collapseClip;

    public void PlayButtonAudio() {
        PlayAudio(_buttonClip);
    }

    public void PlayWinAudio() {
        PlayAudio(_winClip);
    }

    public void PlayLoseAudio() {
        PlayAudio(_loseClip);
    }

    public void PlayCollapseClip() {
        //PlayAudio(_collapseClip);
    }

    private void PlayAudio(AudioClip clip) {
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
    }
}
