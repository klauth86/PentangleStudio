using UnityEngine;

namespace Managers {

    public enum AudioClipType {
        ButtonClick, Collapse, Explode, Lose, Win
    }

    public class ManagerAudio : Base {

        #region Inspector

        [SerializeField] private AudioClip _buttonClickClip;
        [SerializeField] private AudioClip _winClip;
        [SerializeField] private AudioClip _loseClip;
        [SerializeField] private AudioClip _collapseClip;
        [SerializeField] private AudioClip _explodeClip;

        #endregion

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
                        DebugMessage.LogNotSetupWarningMessage("ButtonClickClip");
                    break;

                case AudioClipType.Collapse:
                    if (_collapseClip)
                        PlayAudio(_collapseClip);
                    else
                        DebugMessage.LogNotSetupWarningMessage("CollapseClip");
                    break;

                case AudioClipType.Explode:
                    if (_explodeClip)
                        PlayAudio(_explodeClip);
                    else
                        DebugMessage.LogNotSetupWarningMessage("ExplodeClip");
                    break;

                case AudioClipType.Lose:
                    if (_loseClip)
                        PlayAudio(_loseClip);
                    else
                        DebugMessage.LogNotSetupWarningMessage("LoseClip");
                    break;

                case AudioClipType.Win:
                    if (_winClip)
                        PlayAudio(_winClip);
                    else
                        DebugMessage.LogNotSetupWarningMessage("WinClip");
                    break;
            }
        }        

        #endregion        
    }
}

