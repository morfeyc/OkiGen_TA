namespace CodeBase.Infrastructure.GameStateMachine.States
{
  public interface IState : IExitableState
  {
    void Enter();
  }

  public interface IPayloadedState<TPayload> : IExitableState
  {
    void Enter(TPayload payload);
  }

  public interface ITickableState : IState
  {
    void Tick();
  }
  
  public interface IExitableState
  {
    void Exit();
  }
}