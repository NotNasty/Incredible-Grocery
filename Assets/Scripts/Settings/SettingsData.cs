using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IncredibleGrocery
{
    public struct SettingsData
    {
        public bool MusicOn;
        public bool SoundsOn;

        public SettingsData(bool musicOn, bool soundsOn)
        {
            MusicOn = musicOn;
            SoundsOn = soundsOn;
        }
    }
}
