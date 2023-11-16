using System.Collections.Generic;
using UnityEngine;

namespace IncredibleGrocery.Audio
{
    [CreateAssetMenu(fileName = "New Audio List", menuName = "Audio List")]
    public class AudioAssets : ScriptableObject
    {
        public List<Sound> sounds;
    }
}