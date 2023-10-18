using System.Collections.Generic;

namespace IncredibleGrocery
{
    public abstract class StorageModelParent
    {
        protected static List<ProductSO> SelectedProducts;

        public abstract bool CheckOrder(HashSet<ProductSO> order, ref int price);
    }
}

