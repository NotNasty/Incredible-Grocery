using DG.Tweening;
using IncredibleGrocery.Audio;
using UnityEngine;

namespace IncredibleGrocery.Clouds
{
    public abstract class CloudBase : MonoBehaviour
    {
        [SerializeField] protected Transform cloudGridContent;
        
        private readonly Vector3 appearedCloudScale = Vector3.one;
        private readonly Vector3 appearedCloudRotation = Vector3.zero;
        private readonly Vector3 disappearedCloudScale = new(1, 1, 50);
        private const float AnimationDuration = 0.5f;
        
        private void Awake()
        {
            AudioManager.Instance.PlaySound(AudioTypeEnum.CloudAppeared);
            transform.localScale = Vector3.zero;
            PlayAnimation(true);
        }
        
        public void RemoveCloud()
        {
            AudioManager.Instance.PlaySound(AudioTypeEnum.CloudDisappeared);
            PlayAnimation(false);
        }

        private void PlayAnimation(bool appear)
        {
            var sequence = DOTween.Sequence();
            var ease = appear ? Ease.OutBack : Ease.InBack;
            
            sequence.Append(transform.DOScale(appear ? appearedCloudScale : Vector3.zero, AnimationDuration)
                .SetEase(ease));
            sequence.Join(transform.DORotate(appear ? appearedCloudRotation :disappearedCloudScale, AnimationDuration)
                .SetEase(ease));
            
            if (!appear)
                sequence.OnComplete(() => Destroy(gameObject));
        }
    }
}