using System;
using UnityEngine;

namespace IncredibleGrocery.ClientLogic
{
    [RequireComponent(typeof(Animator))]
    public class ClientAnimationManager : MonoBehaviour
    {
        private Animator _animator;
        private SpriteRenderer _sprite;

        public void Init()
        {
            _animator = GetComponent<Animator>();
            _sprite = GetComponent<SpriteRenderer>();
        }

        public void SetAnimation(ClientAnimationType clientAnimationType)
        {
            switch (clientAnimationType)
            {
                case ClientAnimationType.Leaving:
                    Leaving();
                    break;
                case ClientAnimationType.Waiting:
                    StartWaiting();
                    break;
                case ClientAnimationType.Walking:
                    Walking();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(clientAnimationType), clientAnimationType, null);
            }
        }

        private void Walking()
        {
            _animator.SetBool(Constants.IsWaiting, false);
        }

        private void StartWaiting()
        {
            _animator.SetBool(Constants.IsWaiting, true);
        }

        private void Leaving()
        {
             _animator.SetBool(Constants.IsWaiting, false);
             _sprite.sortingLayerID = SortingLayer.NameToID(Constants.LeavingClientSortingLayer);
            _sprite.flipX = true;
        }
    }
}

