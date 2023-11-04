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
            EventBus.Instance.OnCloudAppeared();
        }
        
        public void RemoveCloud()
        {
            EventBus.Instance.OnCloudDisappeared();
            _animator.Play(Constants.Disappear);
        }
    }
}