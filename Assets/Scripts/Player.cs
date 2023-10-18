using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace IncredibleGrocery
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private SellCloud cloudPrefab;

        private void OnEnable()
        {
            EventBus.Instance.OrderChecked += SellAsync;
        }

        private async void SellAsync(Dictionary<ProductSO, bool> checkedOrder)
        {
            var cloud = Instantiate(cloudPrefab, transform);
            cloud.AddSales(checkedOrder);
            await Task.Delay(Constants.OneSecInMilliseconds);
            cloud.RevealReaction();
            await Task.Delay(Constants.OneSecInMilliseconds);
            EventBus.Instance.OnSaleResultRevealed();
            await Task.Delay(Constants.OneSecInMilliseconds);
            cloud.RemoveCloud();
        }

        private void OnDisable()
        {
            EventBus.Instance.OrderChecked -= SellAsync;
        }
    }
}
