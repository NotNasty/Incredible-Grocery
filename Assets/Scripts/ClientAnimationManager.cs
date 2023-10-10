using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ClientAnimationManager : MonoBehaviour
{
    private Animator _animator;

    private const string IS_WAITING_PARAM = "isWaiting";

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void StartWaiting()
    {
        _animator.SetBool(IS_WAITING_PARAM, true);
    }
}
