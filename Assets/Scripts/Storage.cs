using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class Storage : MonoBehaviour
{
    [SerializeField] private List<ProductSO> products;
    [SerializeField] private Transform StorageGridContent;
    [SerializeField] private GameObject ProductItem;

    public static Storage Instance;

    private void Awake()
    {
        Instance = this;
    }

    public List<ProductSO> Products
    {
        get => products;
        private set => products = value;
    }

    public void ShowStorage()
    {
        foreach (ProductSO product in Products)
        {
            GameObject productButton = Instantiate(ProductItem, StorageGridContent);
            productButton.GetComponentInChildren<Image>().sprite = product.ProductImage;
            //productButton.sp

        }
    }
}
