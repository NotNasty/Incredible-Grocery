namespace IncredibleGrocery.ClientLogic.States
{
    public class ClientGoingToSeller : ClientBaseState
    {
        public ClientGoingToSeller(Client client, ClientStateMachine clientStateMachine) : base(client, clientStateMachine)
        { }

        public override void EnterState()
        {
            SetAnimation(ClientAnimationType.Walking);
        }

        public override void UpdateState()
        {
            if (client.MoveClient(client.TargetPosition))
            {
                clientStateMachine.SetState(client.ClientOrdering);
            }
        }
    }
}
