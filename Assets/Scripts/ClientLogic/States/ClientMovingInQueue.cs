namespace IncredibleGrocery.ClientLogic.States
{
    public class ClientMovingInQueue : ClientBaseState
    {
        public ClientMovingInQueue(Client client, ClientStateMachine clientStateMachine) : base(client, clientStateMachine)
        { }
        
        public override void EnterState()
        {
            SetAnimation(ClientAnimationType.Walking);
        }

        public override void UpdateState()
        {
            if (client.MoveClient(client.TargetPosition))
            {
                clientStateMachine.SwitchState(client.ClientWaitingInQueue);
            }
        }
    }
}
