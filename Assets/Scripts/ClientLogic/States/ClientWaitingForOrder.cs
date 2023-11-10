namespace IncredibleGrocery.ClientLogic.States
{
    public class ClientWaitingForOrder : ClientBaseState
    {
        public ClientWaitingForOrder(Client client, ClientStateMachine clientStateMachine) : base(client, clientStateMachine)
        { }
        
        public override void EnterState()
        {
            SetAnimation(ClientAnimationType.Waiting);
        }

        public override void UpdateState()
        { }
    }
}
