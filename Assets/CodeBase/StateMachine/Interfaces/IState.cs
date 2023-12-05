namespace CodeBase.StateMachine.Interfaces
{
    public interface IState : IExitableState
    {
        public void Enter();
    }
}