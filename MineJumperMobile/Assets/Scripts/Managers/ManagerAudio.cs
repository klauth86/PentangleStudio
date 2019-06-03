using UnityEngine;

namespace Managers {
    [RequireComponent(typeof(Master))]
    public class ManagerAudio : MonoBehaviour {

        private Master _master;

        [SerializeField] private AudioClip _buttonClickClip;
        [SerializeField] private AudioClip _winClip;
        [SerializeField] private AudioClip _loseClip;
        [SerializeField] private AudioClip _collapseClip;
        [SerializeField] private AudioClip _explodeClip;

        private void PlayAudio(AudioClip clip) {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
        }

        #region Manager Methods

        public void PlayButtonClickAudio() {
            if (_buttonClickClip)
                PlayAudio(_buttonClickClip);
        }

        public void PlayWinAudio() {
            if (_winClip)
                PlayAudio(_winClip);
        }

        public void PlayLoseAudio() {
            if (_loseClip)
                PlayAudio(_loseClip);
        }

        public void PlayCollapseAudio() {
            if (_collapseClip)
                PlayAudio(_collapseClip);
        }

        public void PlayExplodeAudio() {
            if (_explodeClip)
                PlayAudio(_explodeClip);
        }

        #endregion        
    }
}

