using UnityEngine;

namespace IncredibleGrocery
{
    public static class Constants
    {
        #region SavedData

        public const string MusicOnSettingName = "MusicOn";
        public const string SoundsOnSettingName = "SoundsOn";
        public const string MoneySettingName = "MoneyCount";

        #endregion

        #region Animation

        private const string IsWaitingAnimParam = "isWaiting";
        public static readonly int IsWaiting = Animator.StringToHash(IsWaitingAnimParam);
        
        private const string DisappearAnimation = "Disappearing";
        public static readonly int Disappear = Animator.StringToHash(DisappearAnimation);
        
        private const string IsActiveAnimParam = "isActive";
        public static readonly int IsActive = Animator.StringToHash(IsActiveAnimParam);

        #endregion


        public const int OneSecInMilliseconds = 1000;
        public const string MoneyDisplayFormat = "$ {0}";
        public const float DestinationToPlayerLimit = 0.2f;
        public const float InactiveImageTransparency = 0.3f;
    }
}

