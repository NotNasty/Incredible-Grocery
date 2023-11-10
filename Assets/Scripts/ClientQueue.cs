using System.Collections.Generic;
using System.Linq;
using IncredibleGrocery.ClientLogic;
using IncredibleGrocery.Money;
using IncredibleGrocery.Storage;
using UnityEngine;

namespace IncredibleGrocery
{
    public class ClientQueue : MonoBehaviour
    {
        private readonly List<Client> _clients = new();
        private float _timer;
        private int _intervalForNextClient;
        private MoneyManager _moneyManager;
        private StoragePresenter _storagePresenter;
        private Player _player;
        
        [SerializeField] private Transform[] queuePositions;
        [SerializeField] private int maxClients;
        [SerializeField] private Client clientPrefab;

        public void Init(MoneyManager moneyManager, StoragePresenter storagePresenter, Player player)
        {
            _moneyManager = moneyManager;
            _storagePresenter = storagePresenter;
            _player = player;
        }
        
        private void Update()
        {
            _timer += Time.deltaTime;
            if (_clients.Count < maxClients && _timer >= _intervalForNextClient)
            {
                var client = Instantiate(clientPrefab, transform);
                var isFirstInQueue = _clients.Count == 0;
                client.Init(queuePositions[_clients.Count].position, _moneyManager, _storagePresenter,
                    isFirstInQueue);
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
            if (_clients == null) 
                return;

            _clients.Remove(clientLeft);

            for (var i = 0; i < _clients.Count; i++)
            {
                _clients[i].TargetPosition = queuePositions[i].position;
            }

            var curClient = _clients.First();
            if (curClient is not null)
            {
                curClient.GoToSeller();
                _player.CurrentClient = curClient;
            }
        }
    }
}
