namespace CodeBase.Infrastructure.GameStateMachine.Provider
{
  public class GameStateMachineProvider : IGameStateMachineProvider
  {
    public IGameStateMachine Value { get; set; }
  }
}