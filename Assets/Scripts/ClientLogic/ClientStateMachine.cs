using IncredibleGrocery.ClientLogic.States;

namespace IncredibleGrocery.ClientLogic
{
    public class ClientStateMachine
    {
        public ClientBaseState CurrentState { get; private set; }
        
        public void Initialize(ClientBaseState startState)
        {
            CurrentState = startState;
            CurrentState.EnterState();
        }

        public void SwitchState(ClientBaseState state)
        {
            CurrentState = state;
            CurrentState.EnterState();
        }
    }
}
