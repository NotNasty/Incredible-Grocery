using IncredibleGrocery.Audio;
using UnityEngine;
using Zenject;

namespace IncredibleGrocery.Installers
{
    public class AudioManagerInstaller : MonoInstaller
    {
        [SerializeField] private AudioClip music;
        [SerializeField] private AudioAssets audioAssets;
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource soundSource;
        
        public override void InstallBindings()
        {
            Container.Bind<AudioManager>().AsSingle().WithArguments(music, audioAssets, musicSource, soundSource).NonLazy();
        }
    }
}