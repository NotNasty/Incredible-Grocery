using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace IncredibleGrocery
{
    public class OrderMaker : IOrderMaker
    {
        private readonly int _minCountOfOrders;
        private readonly int _maxCountOfOrders;
        private CloudManager _cloudManager;

        public OrderMaker(CloudManager cloudManager, int minCountOfOrders = 1, int maxCountOfOrders = 3)
        {
            _cloudManager = cloudManager;
            _minCountOfOrders = minCountOfOrders;
            _maxCountOfOrders = maxCountOfOrders;
        }

        public void GenerateOrder()
        {
            int countOfOrders = Random.Range(_minCountOfOrders, _maxCountOfOrders);
            HashSet<int> taken = new HashSet<int>();
            while (taken.Count < countOfOrders)
            {
                int pickedUpProduct = Random.Range(0, Storage.Instance.Products.Count - 1);
                int previousTakenCount = taken.Count;
                taken.Add(pickedUpProduct);
                if (taken.Count > previousTakenCount)
                    _cloudManager.AddOrder(Storage.Instance.Products[pickedUpProduct]);
            }
            Storage.Instance.ShowStorage();
        }
    }
}