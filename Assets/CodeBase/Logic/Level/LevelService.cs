using System;
using CodeBase.Infrastructure.GameStateMachine.Provider;
using CodeBase.Infrastructure.GameStateMachine.States;
using CodeBase.Services.PersistentProgress;
using CodeBase.UI.Services.Window;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Logic.Level
{
  public class LevelService : ILevelService
  {
    private readonly IGameStateMachineProvider _stateMachineProvider;
    private readonly IWindowService _windowService;
    private readonly IPersistentProgressService _progressService;

    private const int SecondsBeforeRestart = 4;
    
    public LevelService(IWindowService windowService, IPersistentProgressService progressService)
    {
      _windowService = windowService;
      _progressService = progressService;
      Init();
    }

    private void Init()
    {
      _progressService.Progress.NewTask();
      _progressService.Progress.Task.OnCompleted.AddListener(ReloadLevel);
    }

    private async void ReloadLevel()
    {
      await UniTask.Delay(TimeSpan.FromSeconds(SecondsBeforeRestart));
      _windowService.Open(WindowId.LevelEnd);
    }
  }
}