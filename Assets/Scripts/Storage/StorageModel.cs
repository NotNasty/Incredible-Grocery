using System.Collections.Generic;
using System.Linq;
using IncredibleGrocery.Products;

namespace IncredibleGrocery.Storage
{
    public class StorageModel : StorageModelParent
    {
        public StorageModel()
        {
            SelectedProducts = new List<ProductSO>();
        }

        public static void SelectNewProduct(ProductSO product)
        {
            SelectedProducts.Add(product);
            EventBus.Instance.OnSelectedProductsChanged(SelectedProducts.Count);
        }

        public static void UnselectNewProduct(ProductSO product)
        {
            SelectedProducts.Remove(product);
            EventBus.Instance.OnSelectedProductsChanged(SelectedProducts.Count);
        }

        public override bool CheckOrder(HashSet<ProductSO> order, ref int price)
        {
            bool orderIsAllCorrect = true;
            var checkedOrder = new Dictionary<ProductSO, bool>();
            foreach (var selectedProduct in SelectedProducts)
            {
                foreach (var orderedProduct in order.Where(product => product.Equals(selectedProduct)))
                {
                    checkedOrder.Add(selectedProduct, true);
                    price += orderedProduct.price;
                    break;
                }

                if (!checkedOrder.ContainsKey(selectedProduct))
                {
                    orderIsAllCorrect = false;
                    checkedOrder.Add(selectedProduct, false);
                }
            }
            EventBus.Instance.OnOrderChecked(checkedOrder);
            return orderIsAllCorrect;
        }
    }
}