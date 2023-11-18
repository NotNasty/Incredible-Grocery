using System;
using UnityEngine;

namespace IncredibleGrocery.Audio
{
    [Serializable]
    public class Sound
    {
        [field: SerializeField] public AudioClip AudioClip { get; private set; }
        [field:SerializeField]  public AudioTypeEnum AudioType { get; private set; }
    }
}
