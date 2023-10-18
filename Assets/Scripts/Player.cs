using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace IncredibleGrocery
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private SellCloud cloudPrefab;
        private const int DELAY_OF_REACTION = 1000;

        public static event Action SaleResultRevealed;

        private void OnEnable()
        {
            Client.OrderChecked += SellAsync;
        }

        private async void SellAsync(Dictionary<ProductSO, bool> checkedOrder)
        {
            var cloud = Instantiate(cloudPrefab, transform);
            cloud.AddSales(checkedOrder);
            await Task.Delay(DELAY_OF_REACTION);
            cloud.RevealReaction();
            await Task.Delay(DELAY_OF_REACTION);
            SaleResultRevealed?.Invoke();
            await Task.Delay(DELAY_OF_REACTION);
            cloud.RemoveCloud();
        }

        private void OnDisable()
        {
            Client.OrderChecked -= SellAsync;
        }
    }
}
