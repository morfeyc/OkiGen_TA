using CodeBase.Infrastructure.GameStateMachine.Provider;
using CodeBase.Services.StaticData;

namespace CodeBase.Infrastructure.GameStateMachine.States
{
  public class BootstrapState : IState
  {
    private const string FirstLevelName = "Main";
    
    private readonly IGameStateMachineProvider _gameStateMachineProvider;

    public BootstrapState(IGameStateMachineProvider gameStateMachineProvider)
    {
      _gameStateMachineProvider = gameStateMachineProvider;
    }
    
    public void Enter()
    {
      _gameStateMachineProvider.Value
        .Enter<LoadLevelState, string>(FirstLevelName);
    }

    public void Exit()
    {
    }
  }
}