using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IncredibleGrocery
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private GameObject storagePrefab;
        [SerializeField] private Transform storageParent;

        private void Start()
        {
            GameObject storage = Instantiate(storagePrefab, storageParent);
            var storageView = storage.GetComponent<Storage>();
            StoragePresenter storagePresenter = new StoragePresenter(storageView);
        }
    }
}
