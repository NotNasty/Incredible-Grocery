namespace IncredibleGrocery.ClientLogic.States
{
    public class ClientWaitingInQueue : ClientBaseState
    {
        public ClientWaitingInQueue(Client client, ClientStateMachine clientStateMachine) : base(client, clientStateMachine)
        { }
        
        public override void EnterState()
        {
            client.ProgressBar.WaitingTimeEnded += WaitingIsOver;
            SetAnimation(ClientAnimationType.Waiting);
        }

        public override void UpdateState()
        {
            client.ProgressBar.UpdateProgressBar();
        }

        private void WaitingIsOver()
        {
            client.LeaveOnEndWaitingTime();
        }
    }
}
