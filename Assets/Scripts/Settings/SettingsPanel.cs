using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IncredibleGrocery
{
    public class SettingsPanel : MonoBehaviour
    {
        private bool _soundOn;
        private bool _musicON;

        [SerializeField] private Button _saveButton;

        public void Init()
        {
            _saveButton.onClick.AddListener(OnSaveClick);
        }

        public void SetActive(bool activate)
        {
            if (gameObject.activeSelf != activate)
            {
                gameObject.SetActive(activate);
            }
        }

        public void OnSaveClick()
        {
            SetActive(false);
            EventBus.Instance.OnButtonClicked();
        }
    }

}
