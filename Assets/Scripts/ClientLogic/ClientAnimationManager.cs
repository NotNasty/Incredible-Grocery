using DG.Tweening;
using UnityEngine;

namespace IncredibleGrocery.ClientLogic
{
    public class ClientAnimationManager : MonoBehaviour
    {
        private Animator _animator;
        private SpriteRenderer _sprite;
        private Tweener _currentLoopTween;

        private readonly Vector2 walkingAnimPosition = Vector2.up * 0.5f;
        private readonly Vector2 waitingAnimScale = Vector3.one * 0.05f;

        private const float WalkingCycleDuration = 0.25f;
        private const float WaitingCycleDuration = 1f;

        public void Init()
        {
            _sprite = GetComponent<SpriteRenderer>();
        }

        public void SetAnimation(ClientAnimationType clientAnimationType)
        {
            _currentLoopTween?.Kill();
            if (clientAnimationType != ClientAnimationType.Waiting)
            {
                _currentLoopTween = transform.DOPunchPosition(walkingAnimPosition, WalkingCycleDuration, 0)
                    .SetLoops(-1, LoopType.Yoyo);
            }
            else
            {
                _currentLoopTween = transform.DOPunchScale(waitingAnimScale, WaitingCycleDuration, 0, 0)
                    .SetLoops(-1, LoopType.Yoyo);
            }

            if (clientAnimationType == ClientAnimationType.Leaving)
            {
                _sprite.sortingLayerID = SortingLayer.NameToID(Constants.LeavingClientSortingLayer);
                _sprite.flipX = true;
            }
        }
    }
}