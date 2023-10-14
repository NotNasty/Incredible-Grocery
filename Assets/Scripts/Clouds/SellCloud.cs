using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace IncredibleGrocery
{
    public class SellCloud : MonoBehaviour
    {
        [SerializeField] private GameObject orderPrefab;
        [SerializeField] private Transform orderGridContent;

        private const string DISAPPEAR_ANIM = "Disappearing";
        private const int HALF_OF_SECOND = 500;

        private Animator _animator;
        private List<SellToggleButton> _saleButtons = new List<SellToggleButton>();


        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        public void AddSales(Dictionary<ProductSO, bool> checkedOrder)
        {
            foreach (var product in checkedOrder)
            {
                GameObject sellItem = Instantiate(orderPrefab, orderGridContent);
                sellItem.GetComponent<Image>().sprite = product.Key.ProductImage;
                var saleButton = sellItem.GetComponent<SellToggleButton>();
                saleButton.IsRightSale = product.Value;
                _saleButtons.Add(saleButton);
            }
        }

        public async void RevealReaction()
        {
            foreach(var saleButton in _saleButtons)
            {
                await Task.Delay(HALF_OF_SECOND);
                saleButton.ReactionReveal();
            }
        }

        public void RemoveCloud()
        {
            _animator.Play(DISAPPEAR_ANIM);
        }
    }
}
