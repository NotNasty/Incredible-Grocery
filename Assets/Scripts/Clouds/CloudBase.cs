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
            SoundPlayer.PlayCloudAppeared();
        }
        
        public void RemoveCloud()
        {
            SoundPlayer.PlayCloudDisappeared();
            _animator.Play(Constants.Disappear);
        }
    }
}