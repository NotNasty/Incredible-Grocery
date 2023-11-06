using System;
using IncredibleGrocery.Money;
using UnityEngine;

namespace IncredibleGrocery.Settings
{
    public class SaveDataManager : IDisposable
    {
        private SaveData _saveData;

        public SaveDataManager()
        {
            _saveData = new SaveData();
            LoadSavedData();

            MoneyManager.BalanceChanged += SaveMoneyData;
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

        private void SaveMoneyData(int moneyCount)
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

        public void Dispose()
        {
            MoneyManager.BalanceChanged -= SaveMoneyData;
        }
    }

    public struct SaveData
    {
        public int MoneyCount;
        public SettingsData SettingsData;
    }
}
