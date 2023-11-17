using IncredibleGrocery.ClientLogic.States;

namespace IncredibleGrocery.ClientLogic
{
    public class ClientStateMachine
    {
        public ClientBaseState CurrentState { get; private set; }

        public void SetState(ClientBaseState state)
        {
            CurrentState = state;
            CurrentState.EnterState();
        }
    }
}
