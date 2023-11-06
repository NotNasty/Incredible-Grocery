namespace IncredibleGrocery.Audio
{
    public class SoundPlayer
    {
        private static AudioManager _audioManager;
        
        public SoundPlayer(AudioManager audioManager)
        {
            _audioManager = audioManager;
        }

        public static void PlayButtonClicked()
        {
            _audioManager.PlaySound(AudioTypeEnum.ButtonClicked);
        }
        
        public static void PlayCloudAppeared()
        {
            _audioManager.PlaySound(AudioTypeEnum.CloudAppeared);
        }
        
        public static void PlayCloudDisappeared()
        {
            _audioManager.PlaySound(AudioTypeEnum.CloudDisappeared);
        }
        
        public static void PlayMoneyPaid()
        {
            _audioManager.PlaySound(AudioTypeEnum.MoneyPaid);
        }
        
        public static void PlayProductSelected()
        {
            _audioManager.PlaySound(AudioTypeEnum.ProductSelected);
        }
    }
}