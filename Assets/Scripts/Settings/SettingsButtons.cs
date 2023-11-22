using IncredibleGrocery.ToggleButtons;

namespace IncredibleGrocery.Settings
{
    public struct SettingsButtons
    {
        public readonly OffOnButton SoundsButton;
        public readonly OffOnButton MusicButton;

        public SettingsButtons(OffOnButton soundToggle, OffOnButton musicToggle)
        {
            SoundsButton = soundToggle;
            MusicButton = musicToggle;
        }
    }
}