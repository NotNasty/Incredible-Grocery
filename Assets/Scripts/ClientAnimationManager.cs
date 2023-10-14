using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ClientAnimationManager : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _sprite;

    private const string IS_WAITING_PARAM = "isWaiting";

    public void Init()
    {
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    public void StartWaiting()
    {
        _animator.SetBool(IS_WAITING_PARAM, true);
    }

    public void Leaving()
    {
       _sprite.flipX = true;
    }
}
