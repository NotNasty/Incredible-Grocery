namespace IncredibleGrocery.ClientLogic.States
{
    public class ClientOrdering : ClientBaseState
    {
        public ClientOrdering(Client client, ClientStateMachine clientStateMachine) : base(client, clientStateMachine)
        { }

        public override void EnterState()
        {
            SetAnimation(ClientAnimationType.Waiting);
        }

        public override void UpdateState()
        {
            client.OrderAndWait();
        }
    }
}
