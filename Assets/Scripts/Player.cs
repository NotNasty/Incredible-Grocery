using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Burst.CompilerServices;
using UnityEngine;

namespace IncredibleGrocery
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private SellCloud cloudPrefab;

        public static event Action SaleResultRevealed;

        private void OnEnable()
        {
            Client.OrderChecked += SellAsync;
        }

        private async void SellAsync(Dictionary<ProductSO, bool> checkedOrder)
        {
            var cloud = Instantiate(cloudPrefab, transform);
            cloud.AddSales(checkedOrder);
            await Task.Delay(Constants.OneSecInMilliseconds);
            cloud.RevealReaction();
            await Task.Delay(Constants.OneSecInMilliseconds);
            SaleResultRevealed?.Invoke();
            await Task.Delay(Constants.OneSecInMilliseconds);
            cloud.RemoveCloud();
        }

        private void OnDisable()
        {
            Client.OrderChecked -= SellAsync;
        }
    }
}
