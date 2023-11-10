namespace IncredibleGrocery.ClientLogic.States
{
    public class ClientWaitingInQueue : ClientBaseState
    {
        private readonly ClientProgressBar _progressBar;

        public ClientWaitingInQueue(Client client, ClientStateMachine clientStateMachine) : base(client, clientStateMachine)
        {
            _progressBar = client.GetComponentInChildren<ClientProgressBar>();
        }
        
        public override void EnterState()
        {
            _progressBar.WaitingTimeEnded += WaitingIsOver;
            SetAnimation(ClientAnimationType.Waiting);
        }

        public override void UpdateState()
        {
            _progressBar.UpdateProgressBar();
        }

        private void WaitingIsOver()
        {
            client.LeaveOnEndWaitingTime();
        }
    }
}
