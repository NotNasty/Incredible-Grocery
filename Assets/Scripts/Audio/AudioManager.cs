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
            if (musicOn)
            {
                musicSource.Play();
            }
        }

        public void PlaySound(AudioTypeEnum audioType)
        {
            if (!soundOn)
                return;
            
            var sound = sounds.SingleOrDefault(x => x.audioType == audioType);
            Assert.IsNotNull(sound);
            soundSource.PlayOneShot(sound.audioClip);
        }

        public void OnOffMusic(bool isMusicOn)
        {
            musicOn = isMusicOn;
            musicSource.mute = !musicOn;
            PlayMusic();
        }

        public void OnOffSounds(bool soundsOn)
        {
            soundOn = soundsOn;
            soundSource.mute = !soundsOn;
        }
    }
}

