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
            _animator.SetBool(Constants.IsWaiting, clientAnimationType == ClientAnimationType.Waiting);
            if (clientAnimationType == ClientAnimationType.Leaving)
            {
                _sprite.sortingLayerID = SortingLayer.NameToID(Constants.LeavingClientSortingLayer);
                _sprite.flipX = true;
            }
        }
    }
}

