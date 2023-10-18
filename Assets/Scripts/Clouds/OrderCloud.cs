using UnityEngine;
using UnityEngine.UI;

namespace IncredibleGrocery
{
    public class OrderCloud : MonoBehaviour
    {
        [SerializeField] private GameObject orderPrefab;
        [SerializeField] private Transform orderGridContent;

        private Animator _animator;
        private const string DISAPPEAR_ANIM = "Disappearing";

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            EventBus.Instance.OnCloudAppeared();
        }

        public void AddOrder(ProductSO product)
        {
            GameObject orderItem = Instantiate(orderPrefab, orderGridContent);
            orderItem.GetComponent<Image>().sprite = product.ProductImage;
        }

        public void AddReaction(Sprite reaction)
        {
            GameObject orderItem = Instantiate(orderPrefab, orderGridContent);
            orderItem.GetComponent<Image>().sprite = reaction;
        }

        public void RemoveCloud()
        {
            EventBus.Instance.OnCloudDisappeared();
            _animator.Play(DISAPPEAR_ANIM);
        }
    }
}
