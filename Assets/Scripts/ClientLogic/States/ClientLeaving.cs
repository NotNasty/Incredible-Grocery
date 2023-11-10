using UnityEngine;

namespace IncredibleGrocery.ClientLogic.States
{
    public class ClientLeaving : ClientBaseState
    {
        private Vector3 targetExitPosition;
        
        public ClientLeaving(Client client, ClientStateMachine clientStateMachine, Vector3 startPosition) : base(client, clientStateMachine)
        {
            targetExitPosition = startPosition;
        }
        
        public override void EnterState()
        {
            SetAnimation(ClientAnimationType.Leaving);
        }

        public override void UpdateState()
        {
            if (client.MoveClient(targetExitPosition))
            {
                Object.Destroy(client.gameObject);
            }
        }
    }
}
