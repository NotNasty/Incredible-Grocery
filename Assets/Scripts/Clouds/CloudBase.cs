using IncredibleGrocery.Audio;
using UnityEngine;

namespace IncredibleGrocery.Clouds
{
    public abstract class CloudBase : MonoBehaviour
    {
        [SerializeField] protected Transform cloudGridContent;
        
        private Animator _animator;
        
        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            AudioManager.Instance.PlaySound(AudioTypeEnum.CloudAppeared);
        }
        
        public void RemoveCloud()
        {
            AudioManager.Instance.PlaySound(AudioTypeEnum.CloudDisappeared);
            _animator.Play(Constants.Disappear);
        }
    }
}