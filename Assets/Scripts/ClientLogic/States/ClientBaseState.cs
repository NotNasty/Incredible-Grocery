namespace IncredibleGrocery.ClientLogic.States
{
    public abstract class ClientBaseState
    {
        protected readonly Client client;
        protected readonly ClientStateMachine clientStateMachine;

        protected ClientBaseState(Client client, ClientStateMachine clientStateMachine)
        {
            this.client = client;
            this.clientStateMachine = clientStateMachine;
        }

        public abstract void EnterState();
        public abstract void UpdateState();

        protected void SetAnimation(ClientAnimationType clientAnimationType)
        {
            client.animationManager.SetAnimation(clientAnimationType);
        }
    }
}
