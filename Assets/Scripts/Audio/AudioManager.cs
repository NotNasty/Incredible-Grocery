using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace IncredibleGrocery.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public AudioClip music;
        public Sound[] sounds;

        public AudioSource musicSource;
        public AudioSource soundSource;

        [SerializeField] private bool musicOn;
        [SerializeField] private bool soundOn;

        public void Init()
        {
            musicSource.clip = music;
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

        private void PlayMusic()
        {
            if (musicOn)
            {
                musicSource.Play();
            }
        }

        private void PlaySound(AudioTypeEnum audioType)
        {
            if (!soundOn)
                return;
            
            var sound = sounds.SingleOrDefault(x => x.audioType == audioType);
            Assert.IsNotNull(sound);
            soundSource.PlayOneShot(sound.audioClip);
        }

        private void OnOffMusic(bool isMusicOn)
        {
            musicOn = isMusicOn;
            musicSource.mute = !isMusicOn;
            if (isMusicOn)
                PlayMusic();
        }

        private void OnOffSounds(bool soundsOn)
        {
            soundOn = soundsOn;
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

