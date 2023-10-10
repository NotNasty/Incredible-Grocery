using UnityEngine;
using UnityEngine.UI;

namespace IncredibleGrocery
{
    public class CloudManager : MonoBehaviour
    {
        [SerializeField] private GameObject orderPrefab;

        [SerializeField] private Transform orderGridContent;

        public void AddOrder(ProductSO product)
        {
            GameObject orderItem = Instantiate(orderPrefab, orderGridContent);
            orderItem.GetComponent<Image>().sprite = product.ProductImage;
        }
    }
}
