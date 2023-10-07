using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ClientAnimationManager : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void StartWaiting()
    {
        //Debug.Log("StartWaiting");
        _animator.SetBool("isWaiting", true);
    }
}
