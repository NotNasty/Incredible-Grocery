using UnityEngine;

namespace IncredibleGrocery
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

        public void StartWaiting()
        {
            _animator.SetBool(Constants.IsWaitingAnimParam, true);
        }

        public void Leaving()
        {
             _animator.SetBool(Constants.IsWaitingAnimParam, false);
            _sprite.flipX = true;
        }
    }
}

