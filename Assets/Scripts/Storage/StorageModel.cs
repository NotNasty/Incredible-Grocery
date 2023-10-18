using System.Collections.Generic;

namespace IncredibleGrocery
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
            Dictionary<ProductSO, bool> checkedOrder = new Dictionary<ProductSO, bool>();
            foreach (var selectedProduct in StorageModel.SelectedProducts)
            {
                foreach (var orderedProduct in order)
                {
                    if (orderedProduct.Equals(selectedProduct))
                    {
                        checkedOrder.Add(selectedProduct, true);
                        price += orderedProduct.Price;
                        break;
                    }
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