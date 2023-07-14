using System.Threading.Tasks;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.GameStateMachine.Provider;
using CodeBase.Services.Assets;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SceneLoader;
using CodeBase.Services.StaticData;
using CodeBase.StaticData;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Services.Window;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.GameStateMachine.States
{
  public class LoadLevelState : IPayloadedState<string>
  {
    private readonly IGameStateMachineProvider _stateMachineProvider;
    private readonly ISceneLoaderService _sceneLoader;
    private readonly IPersistentProgressService _progressService;
    private readonly IStaticDataService _staticDataService;
    private readonly IAssetProvider _assetProvider;
    private readonly IGameFactory _gameFactory;
    private readonly IUIFactory _uiFactory;
    private readonly IWindowService _windowService;

    public LoadLevelState(IGameStateMachineProvider stateMachineProvider,
      ISceneLoaderService sceneLoader,
      IAssetProvider assetProvider,
      IGameFactory gameFactory,
      IUIFactory uiFactory,
      IWindowService windowService,
      IPersistentProgressService progressService,
      IStaticDataService staticDataService)
    {
      _stateMachineProvider = stateMachineProvider;
      _sceneLoader = sceneLoader;
      _assetProvider = assetProvider;
      _gameFactory = gameFactory;
      _uiFactory = uiFactory;
      _windowService = windowService;
      _progressService = progressService;
      _staticDataService = staticDataService;
    }

    public void Enter(string payload)
    {
      _gameFactory.Cleanup();
      _assetProvider.Cleanup();
      
      _sceneLoader.Load(payload, onLoaded: OnLoaded);
      _stateMachineProvider.Value.Enter<GameLoopState>();
    }

    public void Exit()
    {
    }

    private async void OnLoaded()
    {
      LevelStaticData levelData = LevelStaticData();
      await InitUI();
    }

    private async Task InitUI()
    {
      await _uiFactory.CreateUIRoot();
      await _windowService.Open(WindowId.None);
    }

    private LevelStaticData LevelStaticData() => 
      _staticDataService.ForLevel(SceneManager.GetActiveScene().name);
  }
}