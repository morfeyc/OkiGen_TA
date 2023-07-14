namespace CodeBase.Infrastructure.GameStateMachine.Provider
{
  public interface IGameStateMachineProvider
  {
    IGameStateMachine Value { get; set; }
  }
}