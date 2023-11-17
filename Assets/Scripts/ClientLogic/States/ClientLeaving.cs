using UnityEngine;

namespace IncredibleGrocery.ClientLogic.States
{
    public class ClientLeaving : ClientBaseState
    {
        private readonly Vector3 _targetExitPosition;
        
        public ClientLeaving(Client client, ClientStateMachine clientStateMachine, Vector3 startPosition) : base(client, clientStateMachine)
        {
            _targetExitPosition = startPosition;
        }
        
        public override void EnterState()
        {
            SetAnimation(ClientAnimationType.Leaving);
        }

        public override void UpdateState()
        {
            if (client.MoveClient(_targetExitPosition))
            {
                Object.Destroy(client.gameObject);
            }
        }
    }
}
