using System;
using UnityEngine;

namespace IncredibleGrocery.Settings
{
    public class SaveDataManager
    {
        private SaveData _saveData;

        public SaveDataManager()
        {
            _saveData = new SaveData();
            LoadSavedData();
        }

        private void LoadSavedData()
        {
            _saveData.MoneyCount = PlayerPrefs.GetInt(Constants.MoneySettingName);
            _saveData.SettingsData.MusicOn = Convert.ToBoolean(PlayerPrefs.GetInt(Constants.MusicOnSettingName, 1));
            _saveData.SettingsData.SoundsOn = Convert.ToBoolean(PlayerPrefs.GetInt(Constants.SoundsOnSettingName, 1));
        }

        public SettingsData GetSettingsData()
        {
            return _saveData.SettingsData;
        }

        public int GetMoneyCount()
        {
            return _saveData.MoneyCount;
        }

        public void SaveMoneyData(int moneyCount)
        {
            _saveData.MoneyCount = moneyCount;
            PlayerPrefs.SetInt(Constants.MoneySettingName, _saveData.MoneyCount);
            PlayerPrefs.Save();
        }

        public void SaveSettingsData(SettingsData settingsData)
        {
            _saveData.SettingsData = settingsData;
            PlayerPrefs.SetInt(Constants.MusicOnSettingName, Convert.ToInt32(_saveData.SettingsData.MusicOn));
            PlayerPrefs.SetInt(Constants.SoundsOnSettingName, Convert.ToInt32(_saveData.SettingsData.SoundsOn));
            PlayerPrefs.Save();
        }
    }

    public struct SaveData
    {
        public int MoneyCount;
        public SettingsData SettingsData;
    }
}
