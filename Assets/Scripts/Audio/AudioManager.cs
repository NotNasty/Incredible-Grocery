using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace IncredibleGrocery.Audio
{
    public class AudioManager
    {
        private bool _musicOn;
        private bool _soundOn;

        private readonly AudioAssets _audioAssets;
        private readonly AudioSource _musicSource;
        private readonly AudioSource _soundSource;
        
        public static AudioManager Instance { get; private set; }
        
        public AudioManager(AudioClip music, AudioAssets audioAssets, AudioSource musicSource, AudioSource soundSource)
        {
            Instance ??= this;
            
            _audioAssets = audioAssets;
            _musicSource = musicSource;
            _soundSource = soundSource;
            _musicSource.clip = music;
        }

        private void PlayMusic()
        {
            if (_musicOn)
            {
                _musicSource.Play();
            }
        }

        public void PlaySound(AudioTypeEnum audioType)
        {
            if (!_soundOn)
                return;
            
            var sound = _audioAssets.sounds.SingleOrDefault(x => x.AudioType == audioType);
            Assert.IsNotNull(sound);
            _soundSource.PlayOneShot(sound.AudioClip);
        }

        public void OnOffMusic(bool isMusicOn)
        {
            _musicOn = isMusicOn;
            _musicSource.mute = !_musicOn;
            PlayMusic();
        }

        public void OnOffSounds(bool soundsOn)
        {
            _soundOn = soundsOn;
            _soundSource.mute = !_soundOn;
        }
    }
}

