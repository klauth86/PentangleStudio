using UnityEngine;

namespace Managers {
    public class AudioManager : MonoBehaviour {

        [SerializeField] private AudioClip _buttonClip;
        [SerializeField] private AudioClip _winClip;
        [SerializeField] private AudioClip _loseClip;
        [SerializeField] private AudioClip _collapseClip;
        [SerializeField] private AudioClip _explodeClip;

        public void PlayButtonAudio() {
            if (_buttonClip)
                PlayAudio(_buttonClip);
        }

        public void PlayWinAudio() {
            if (_winClip)
                PlayAudio(_winClip);
        }

        public void PlayLoseAudio() {
            if (_loseClip)
                PlayAudio(_loseClip);
        }

        public void PlayCollapseClip() {
            if (_collapseClip)
                PlayAudio(_collapseClip);
        }

        public void PlayExplodeClip() {
            if (_explodeClip)
                PlayAudio(_explodeClip);
        }

        private void PlayAudio(AudioClip clip) {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
        }
    }
}

