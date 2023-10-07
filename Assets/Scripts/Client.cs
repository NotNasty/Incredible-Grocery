using UnityEngine;

public class Client : MonoBehaviour
{
    [SerializeField]
    private Transform targetPositionForOrdering;

    private ClientAnimationManager _animationManager;
    private bool _isWalking = true;
    private const float DESTIONATION_LIMIT = .2f;

    private void Start()
    {
        _animationManager = GetComponentInChildren<ClientAnimationManager>();
    }

    private void Update()
    {
        if (_isWalking)
        {
            MoveClient();
        }
    }

    public void MoveClient()
    {
        if (Vector2.Distance(transform.position, targetPositionForOrdering.position) > DESTIONATION_LIMIT)
        {
            transform.position = Vector2.Lerp(transform.position, targetPositionForOrdering.position, Time.deltaTime);
        }
        else
        {
            _isWalking = false;
            _animationManager.StartWaiting();
        }
    }
}
