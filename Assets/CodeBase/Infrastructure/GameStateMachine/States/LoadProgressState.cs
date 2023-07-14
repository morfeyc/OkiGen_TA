using CodeBase.Data;
using CodeBase.Infrastructure.GameStateMachine.Provider;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;

namespace CodeBase.Infrastructure.GameStateMachine.States
{
  public class LoadProgressState : IState
  {
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
      
      _stateMachineProvider.Value.Enter<LoadLevelState, string>("SampleScene");
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