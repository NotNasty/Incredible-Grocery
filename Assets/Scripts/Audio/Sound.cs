using System;
using UnityEngine;

namespace IncredibleGrocery.Audio
{
    [Serializable, CreateAssetMenu(fileName = "New Sound", menuName = "Sound")]
    public class Sound : ScriptableObject
    {
        public AudioClip audioClip;
        public AudioTypeEnum audioType;
    }
}
