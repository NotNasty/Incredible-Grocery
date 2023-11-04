using System.Collections.Generic;
using IncredibleGrocery.Products;

namespace IncredibleGrocery.Storage
{
    public abstract class StorageModelParent
    {
        protected static List<ProductSO> SelectedProducts;

        public abstract bool CheckOrder(HashSet<ProductSO> order, ref int price);
    }
}

