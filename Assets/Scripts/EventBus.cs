using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IncredibleGrocery
{
    public class EventBus
    {
        private static EventBus _instance;

        public static EventBus Instance
        {
            get
            {
                if (_instance is null)
                {
                    _instance = new EventBus();
                }
                return _instance;
            }
        }


        private EventBus() { }

        public event Action<AudioTypeEnum> ButtonClicked;
        public event Action<AudioTypeEnum> CloudAppeared;
        public event Action<AudioTypeEnum> CloudDisappeared;
        public event Action<AudioTypeEnum> MoneyPaid;
        public event Action<AudioTypeEnum> ProductSelected;

        public void OnButtonClicked()
        {
            ButtonClicked?.Invoke(AudioTypeEnum.ButtonClicked);
        }

        public void OnCloudAppeared()
        {
            CloudAppeared?.Invoke(AudioTypeEnum.CloudAppeared);
        }

        public void OnCloudDisappeared()
        {
            CloudDisappeared?.Invoke(AudioTypeEnum.CloudDisappeared);
        }

        public void OnMoneyPaid()
        {
            MoneyPaid?.Invoke(AudioTypeEnum.MoneyPaid);
        }

        public void OnProductSelected()
        {
            ProductSelected?.Invoke(AudioTypeEnum.ProductSelected);
        }
    }
}