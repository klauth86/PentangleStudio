using System;
using MineJumperMobile_2019.Dicts;
using UnityEngine;

namespace MineJumperMobile_2019.Masters {
    class SubAudio : Sub {

        #region Inspector

        [SerializeField] private AudioClip _buttonClip;
        [SerializeField] private AudioClip _winClip;
        [SerializeField] private AudioClip _loseClip;
        [SerializeField] private AudioClip _collapseClip;
        [SerializeField] private AudioClip _explodeClip;

        #endregion

        private void OnEnable() {
            Master.ButtonActionEvent += OnButtonActionEvent;
            Master.GameOverEvent += OnGameOverEvent;
        }

        private void OnDisable() {
            Master.ButtonActionEvent -= OnButtonActionEvent;
            Master.GameOverEvent -= OnGameOverEvent;
        }

        private void OnButtonActionEvent(ButtonAction param) {
            PlayAudio(_buttonClip)
        }

        private void OnGameOverEvent(bool win) {
            if (win)
                PlayAudio(_winClip);
            else
                PlayAudio(_loseClip);
        }

        private void PlayAudio(AudioClip clip) {
            if (clip)
                AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
            else
                DebugMessage.LogNotSetupWarningMessage(clip.name);
        }
    }
}
