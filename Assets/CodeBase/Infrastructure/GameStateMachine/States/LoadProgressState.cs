using CodeBase.Data;
using CodeBase.Infrastructure.GameStateMachine.Provider;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;

namespace CodeBase.Infrastructure.GameStateMachine.States
{
  public class LoadProgressState : IState
  {
    private const string FirstScene = "Main";
    
    private readonly IGameStateMachineProvider _stateMachineProvider;
    private readonly IPersistentProgressService _progressService;
    private readonly ISaveLoadService _saveLoadService;

    public LoadProgressState(IGameStateMachineProvider stateMachineProvider, IPersistentProgressService progressService, ISaveLoadService saveLoadService)
    {
      _stateMachineProvider = stateMachineProvider;
      _progressService = progressService;
      _saveLoadService = saveLoadService;
    }
    
    public void Enter()
    {
      LoadProgressOrInitNew();
      
      _stateMachineProvider.Value.Enter<LoadLevelState, string>(FirstScene);
    }

    public void Exit()
    {
    }

    private void LoadProgressOrInitNew()
    {
      _progressService.Progress =
        _saveLoadService.LoadProgress()
        ?? NewProgress();
    }

    private PlayerProgress NewProgress() => 
      new();
  }
}