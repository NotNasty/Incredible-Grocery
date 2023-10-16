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

        [SerializeField] private bool MusicOn;
        [SerializeField] private bool SoundOn;

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
        }


        public void PlayMusic()
        {
            if (MusicOn)
            {
                musicSource.Play();
            }
        }

        public void PlaySound(AudioTypeEnum audioType)
        {
            if (SoundOn)
            {
                var sound = Sounds.SingleOrDefault(x => x.audioType == audioType);
                Assert.IsNotNull(sound);
                
                //soundSource.clip = sound.audioClip;
                soundSource.PlayOneShot(sound.audioClip);
            }
        }

        private void OnDisable() 
        {
            EventBus.Instance.ButtonClicked -= PlaySound;
            EventBus.Instance.CloudAppeared -= PlaySound;
            EventBus.Instance.CloudDisappeared -= PlaySound;
            EventBus.Instance.MoneyPaid -= PlaySound;
            EventBus.Instance.ProductSelected -= PlaySound;
        }
    }
}

