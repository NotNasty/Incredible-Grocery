using UnityEngine;
using UnityEngine.UI;

namespace IncredibleGrocery
{
    [RequireComponent(typeof(Image))]
    public class SellToggleButton : MonoBehaviour
    {
        [SerializeField] private Image negativeReaction;
        [SerializeField] private Image positiveReaction;

        private Image _image;
        private float TRANSPARENT_LEVEL = .3f;

        public bool IsRightSale = false;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public void ReactionReveal()
        {
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, TRANSPARENT_LEVEL);
            if (IsRightSale)
            {
                positiveReaction.gameObject.SetActive(true);
            }
            else
            {
                negativeReaction.gameObject.SetActive(true);
            }
        }
    }
}
