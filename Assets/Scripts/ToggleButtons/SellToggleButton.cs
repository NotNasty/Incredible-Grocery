using UnityEngine;
using UnityEngine.UI;

namespace IncredibleGrocery.ToggleButtons
{
    [RequireComponent(typeof(Image))]
    public class SellToggleButton : MonoBehaviour
    {
        [SerializeField] private Image negativeReaction;
        [SerializeField] private Image positiveReaction;

        private Image _image;
        private bool _isRightSale;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public void SetSaleImages(Sprite productImage, bool isRightSale)
        {
            _image.sprite = productImage;
            _isRightSale = isRightSale;
        }

        public void ReactionReveal()
        {
            _image.SetAlphaChanel(Constants.InactiveImageTransparency);
            positiveReaction.gameObject.SetActive(_isRightSale);
            negativeReaction.gameObject.SetActive(!_isRightSale);
        }
    }
}
