namespace CodeBase.StateMachine.Interfaces
{
    public interface IStateWithPayload<TPayload> : IExitableState
    {
        public void Enter(TPayload payload);
    }
}