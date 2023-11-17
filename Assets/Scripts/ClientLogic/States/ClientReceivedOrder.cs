namespace IncredibleGrocery.ClientLogic.States
{
    public class ClientReceivedOrder : ClientBaseState
    {
        public ClientReceivedOrder(Client client, ClientStateMachine clientStateMachine) : base(client, clientStateMachine)
        { }

        public override void EnterState()
        {
            SetAnimation(ClientAnimationType.Waiting);
        }

        public override void UpdateState()
        { }
    }
}