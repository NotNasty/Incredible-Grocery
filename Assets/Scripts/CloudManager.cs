using UnityEngine;
using UnityEngine.UI;

namespace IncredibleGrocery
{
    public class CloudManager : MonoBehaviour
    {
        [SerializeField] private int _minCountOfOrders;
        [SerializeField] private int _maxCountOfOrders;
        [SerializeField] private GameObject orderPrefab;
        [SerializeField] private Transform orderGridContent;

        private Animator _animator;

        public int MinCountOfOrders { get => _minCountOfOrders; }
        public int MaxCountOfOrders { get => _maxCountOfOrders; }

        private void Start() 
        {
            _animator = GetComponentInChildren<Animator>();
        }

        public void AddOrder(ProductSO product)
        {
            GameObject orderItem = Instantiate(orderPrefab, orderGridContent);
            orderItem.GetComponent<Image>().sprite = product.ProductImage;
        }

        public void RemoveCloud()
        {
            _animator.Play("Disappearing");
        }
    }
}
