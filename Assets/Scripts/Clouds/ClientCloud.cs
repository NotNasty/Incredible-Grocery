using UnityEngine;
using UnityEngine.UI;

namespace IncredibleGrocery.Clouds
{
    public class ClientCloud : CloudBase
    {
        [SerializeField] private Image orderPrefab;

        public void AddImage(Sprite image)
        {
            InstantiateImage().sprite = image;
        }

        private Image InstantiateImage() => Instantiate(orderPrefab, cloudGridContent);
    }
}
