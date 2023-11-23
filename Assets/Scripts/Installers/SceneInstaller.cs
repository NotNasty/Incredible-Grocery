using IncredibleGrocery.Money;
using IncredibleGrocery.Products;
using IncredibleGrocery.Settings;
using IncredibleGrocery.Storage;
using UnityEngine;
using Zenject;

namespace IncredibleGrocery.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private ProductsList products;
        [SerializeField] private Player player;
        [SerializeField] private ClientQueue clientQueue;
        [SerializeField] private StoragesManager storagesManager;
        [SerializeField] private MainScreen mainScreen;
    
        public override void InstallBindings()
        {
            Container.Bind<SaveDataManager>().AsSingle();
            Container.Bind<MoneyManager>().AsSingle();
            Container.Bind<MainScreen>().FromInstance(mainScreen).AsSingle().NonLazy();
            Container.Bind<ProductsList>().FromInstance(products).AsSingle();
            Container.Bind<StoragesManager>().FromInstance(storagesManager).AsSingle();
            
            Container.Bind<Player>().FromInstance(player).AsSingle();
            
            Container.Bind<ClientQueue>().FromInstance(clientQueue).AsSingle();
        }
    }
}