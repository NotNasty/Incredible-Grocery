using System.Collections.Generic;
using IncredibleGrocery.ClientLogic;
using IncredibleGrocery.Products;
using UnityEngine;
using Zenject;

namespace IncredibleGrocery
{
    public class ClientQueue : MonoBehaviour
    {
        [SerializeField] private Transform[] queuePositions;
        [SerializeField] private int maxClients;
        [SerializeField] private Client clientPrefab;
        
        private readonly List<Client> _clients = new();
        private float _timer;
        private int _intervalForNextClient;
        private Player _player;
        private List<Product> _products;

        [Inject]
        public void Init(ProductsList products, Player player)
        {
            _products = products.products;
            _player = player;
        }
        
        private void Update()
        {
            _timer += Time.deltaTime;
            if (_clients.Count < maxClients && _timer >= _intervalForNextClient)
            {
                var client = Instantiate(clientPrefab, transform);
                var isFirstInQueue = _clients.Count == 0;
                client.Init(queuePositions[_clients.Count].position, _products, isFirstInQueue);
                _clients.Add(client);
                client.LeftFromShop += OnClientLeft;
                if (isFirstInQueue)
                {
                    _player.CurrentClient = client;
                }
                
                _timer = 0;
                _intervalForNextClient = Random.Range(5, 11);
            }
        }

        private void OnClientLeft(Client clientLeft)
        {
            _clients.Remove(clientLeft);
            
            if (_clients.Count == 0)
                return;

            for (var i = 0; i < _clients.Count; i++)
            {
                _clients[i].TargetPosition = queuePositions[i].position;
            }

            _clients[0].GoToSeller(); 
            _player.CurrentClient = _clients[0];
        }
    }
}
