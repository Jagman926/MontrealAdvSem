using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{

    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : Singleton<SoundManager>
    {
        AudioSource audioSource;
        [SerializeField]
        private AudioClip jumpSound;
        [SerializeField]
        private AudioClip landSound;
        [SerializeField]
        private AudioClip cheerSound;

        void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void LoadClip(AudioClip audioClip)
        {
            audioSource.clip = audioClip;
        }

        private void PlayAudioClip()
        {
            audioSource.Play();
        }

        private void PlayAudioClipFull(AudioClip audioClip, float volumeScale)
        {
			audioSource.PlayOneShot(audioClip, volumeScale);
        }

        public void LoadAndPlayClip(AudioClip audioClip)
        {
            LoadClip(audioClip);
            PlayAudioClip();
        }

        public void PlayJumpAudio()
        {
            LoadAndPlayClip(jumpSound);
        }

        public void PlayLandAudio()
        {
            PlayAudioClipFull(landSound, 0.5f);
        }

        public void PlayCheerClip()
        {
            PlayAudioClipFull(cheerSound, 0.2f);
        }
    }
}
