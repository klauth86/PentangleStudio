using UnityEngine;

namespace Managers {
    public enum AudioClipType {
        ButtonClick, Collapse, Explode, Lose, Win
    }

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

        public void PlayAudio(AudioClipType clipType) {
            switch (clipType) {
                case AudioClipType.ButtonClick:
                    if (_buttonClickClip)
                        PlayAudio(_buttonClickClip);
                    else
                        Debug.LogWarning("ButtonClickClip is not set up in Inspector");
                    break;

                case AudioClipType.Collapse:
                    if (_collapseClip)
                        PlayAudio(_collapseClip);
                    else
                        Debug.LogWarning("CollapseClip is not set up in Inspector");
                    break;

                case AudioClipType.Explode:
                    if (_explodeClip)
                        PlayAudio(_explodeClip);
                    else
                        Debug.LogWarning("ExplodeClip is not set up in Inspector");
                    break;

                case AudioClipType.Lose:
                    if (_loseClip)
                        PlayAudio(_loseClip);
                    else
                        Debug.LogWarning("LoseClip is not set up in Inspector");
                    break;

                case AudioClipType.Win:
                    if (_winClip)
                        PlayAudio(_winClip);
                    else
                        Debug.LogWarning("WinClip is not set up in Inspector");
                    break;
            }
        }        

        #endregion        
    }
}

