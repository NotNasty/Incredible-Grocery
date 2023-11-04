using System;
using System.Collections.Generic;
using IncredibleGrocery.Audio;
using IncredibleGrocery.Clouds;
using IncredibleGrocery.Products;

namespace IncredibleGrocery
{
    public class EventBus
    {
        private static EventBus _instance;

        public static EventBus Instance => _instance ??= new EventBus();

        public event Action<AudioTypeEnum> ButtonClicked;
        public event Action<AudioTypeEnum> CloudAppeared;
        public event Action<AudioTypeEnum> CloudDisappeared;
        public event Action<AudioTypeEnum> MoneyPaid;
        public event Action<AudioTypeEnum> ProductSelected;

        public event Action<bool> SoundStatusChanged;
        public event Action<bool> MusicStatusChanged;

        public event Action<int> SelectedProductsChanged;
        public event Action<Dictionary<ProductSO, bool>> OrderChecked;
        public event Action<ClientCloud> OrderGenerated;
        public event Action LeftFromShop;
        public event Action<int> BalanceChanged;
        public event Action SaleResultRevealed;
        public event Action SellButtonClicked;
        public event Action<bool> NeededCountOfProducts;

        private EventBus() { }

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

        public void OnSoundsStatusChanged(bool isOn)
        {
            SoundStatusChanged?.Invoke(isOn);
        }

        public void OnMusicStatusChanged(bool isOn)
        {
            MusicStatusChanged?.Invoke(isOn);
        }

        public void OnSelectedProductsChanged(int countOfSelectedProducts)
        {
            SelectedProductsChanged?.Invoke(countOfSelectedProducts);
        }

        public void OnOrderChecked(Dictionary<ProductSO, bool> checkedProducts)
        {
            OrderChecked?.Invoke(checkedProducts);
        }

        public void OnOrderGenerated(ClientCloud cloud)
        {
            OrderGenerated?.Invoke(cloud);
        }

        public void OnLeftFromShop()
        {
            LeftFromShop?.Invoke();
        }

        public void OnBalanceChanged(int balanceChange)
        {
            BalanceChanged?.Invoke(balanceChange);
        }

        public void OnSaleResultRevealed()
        {
            SaleResultRevealed?.Invoke();
        }

        public void OnSellButtonClicked()
        {
            SellButtonClicked?.Invoke();
            OnButtonClicked();
        }

        public void OnNeededCountOfProducts(bool neededCount)
        {
            NeededCountOfProducts?.Invoke(neededCount);
        }
    }
}