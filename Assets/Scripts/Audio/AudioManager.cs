using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.PlayerLoop;
using UnityEngine.Assertions;

namespace IncredibleGrocery
{
    public class AudioManager : MonoBehaviour
    {
        public Sound Music;
        public Sound[] Sounds;

        public AudioSource musicSource;
        public AudioSource soundSource;

        [SerializeField] private bool _musicOn;
        [SerializeField] private bool _soundOn;

        public void Init()
        {
            musicSource.clip = Music.audioClip;
        }

        private void OnEnable()
        {
            EventBus.Instance.ButtonClicked += PlaySound;
            EventBus.Instance.CloudAppeared += PlaySound;
            EventBus.Instance.CloudDisappeared += PlaySound;
            EventBus.Instance.MoneyPaid += PlaySound;
            EventBus.Instance.ProductSelected += PlaySound;
            EventBus.Instance.MusicStatusChanged += OnOffMusic;
            EventBus.Instance.SoundStatusChanged += OnOffSounds;
        }


        public void PlayMusic()
        {
            if (_musicOn)
            {
                musicSource.Play();
            }
        }

        public void PlaySound(AudioTypeEnum audioType)
        {
            if (_soundOn)
            {
                var sound = Sounds.SingleOrDefault(x => x.audioType == audioType);
                Assert.IsNotNull(sound);

                soundSource.PlayOneShot(sound.audioClip);
            }
        }

        public void OnOffMusic(bool musicOn)
        {
            _musicOn = musicOn;
            musicSource.mute = !musicOn;
            if (musicOn)
                PlayMusic();
        }

        public void OnOffSounds(bool soundsOn)
        {
            _soundOn = soundsOn;
            soundSource.mute = !soundsOn;
        }

        private void OnDisable()
        {
            EventBus.Instance.ButtonClicked -= PlaySound;
            EventBus.Instance.CloudAppeared -= PlaySound;
            EventBus.Instance.CloudDisappeared -= PlaySound;
            EventBus.Instance.MoneyPaid -= PlaySound;
            EventBus.Instance.ProductSelected -= PlaySound;
            EventBus.Instance.MusicStatusChanged -= OnOffMusic;
            EventBus.Instance.SoundStatusChanged -= OnOffSounds;
        }
    }
}

