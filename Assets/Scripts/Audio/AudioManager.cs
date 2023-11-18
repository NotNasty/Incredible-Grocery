using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace IncredibleGrocery.Audio
{
    public class AudioManager : MonoBehaviour
    {
        private bool _musicOn;
        private bool _soundOn;
        
        public AudioClip music;
        public AudioAssets audioAssets;
        public AudioSource musicSource;
        public AudioSource soundSource;
        
        public static AudioManager Instance { get; private set; }
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }

            musicSource.clip = music;
        }

        private void PlayMusic()
        {
            if (_musicOn)
            {
                musicSource.Play();
            }
        }

        public void PlaySound(AudioTypeEnum audioType)
        {
            if (!_soundOn)
                return;
            
            var sound = audioAssets.sounds.SingleOrDefault(x => x.AudioType == audioType);
            Assert.IsNotNull(sound);
            soundSource.PlayOneShot(sound.AudioClip);
        }

        public void OnOffMusic(bool isMusicOn)
        {
            _musicOn = isMusicOn;
            musicSource.mute = !_musicOn;
            PlayMusic();
        }

        public void OnOffSounds(bool soundsOn)
        {
            _soundOn = soundsOn;
            soundSource.mute = !_soundOn;
        }
    }
}

