using UnityEngine;

namespace IncredibleGrocery.Clouds
{
    public class CloudFinishedEvent : StateMachineBehaviour
    {
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Destroy(animator.gameObject);
        }
    }
}
