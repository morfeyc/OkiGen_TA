using CodeBase.Infrastructure.GameStateMachine.Provider;

namespace CodeBase.Infrastructure.GameStateMachine.States
{
  public class BootstrapState : IState
  {
    private readonly IGameStateMachineProvider _gameStateMachineProvider;

    public BootstrapState(IGameStateMachineProvider gameStateMachineProvider)
    {
      _gameStateMachineProvider = gameStateMachineProvider;
    }
    
    public void Enter()
    {
      _gameStateMachineProvider.Value.Enter<LoadProgressState>();
    }

    public void Exit()
    {
    }
  }
}