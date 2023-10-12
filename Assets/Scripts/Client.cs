using System;
using UnityEngine;

namespace IncredibleGrocery
{

    public class Client : MonoBehaviour
    {
        public static event Action MakingOrder;

        [SerializeField] private Transform targetPositionForOrdering;
        [SerializeField] private CloudManager cloudPrefab;

        private ClientAnimationManager _animationManager;
        private bool _isWalking = true;
        private const float DESTINATION_LIMIT = .2f;

        private void Start()
        {
            _animationManager = GetComponentInChildren<ClientAnimationManager>();
        }

        private void Update()
        {
            if (_isWalking)
            {
                MoveClient(transform.position, targetPositionForOrdering.position);
            }
        }

        private void MoveClient(Vector3 currentPosition, Vector3 targetPosition)
        {
            if (Vector2.Distance(currentPosition, targetPosition) > DESTINATION_LIMIT)
            {
                transform.position = Vector2.Lerp(transform.position, targetPosition, Time.deltaTime);
            }
            else
            {
                MakeOrder();
            }
        }

        private void MakeOrder()
        {
            _isWalking = false;
            _animationManager.StartWaiting();
            var cloud = Instantiate(cloudPrefab, transform);
            IOrderMaker orderMaker = new OrderMaker(cloud);
            orderMaker.GenerateOrder();
        }
    }
}