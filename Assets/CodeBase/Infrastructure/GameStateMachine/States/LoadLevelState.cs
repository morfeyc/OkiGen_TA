using CodeBase.Infrastructure.Factory.Fruit;
using CodeBase.Services.Assets;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SceneLoader;
using CodeBase.StaticData;
using CodeBase.UI.Services.Window;

namespace CodeBase.Infrastructure.GameStateMachine.States
{
  public class LoadLevelState : IPayloadedState<string>
  {
    private readonly ISceneLoaderService _sceneLoader;
    private readonly IAssetProvider _assetProvider;
    private readonly IFruitFactory _fruitFactory;
    private readonly IWindowService _windowService;
    private readonly IPersistentProgressService _progressService;

    private LevelStaticData _levelData;

    public LoadLevelState(ISceneLoaderService sceneLoader,
      IAssetProvider assetProvider,
      IFruitFactory fruitFactory,
      IWindowService windowService,
      IPersistentProgressService progressService)
    {
      _sceneLoader = sceneLoader;
      _assetProvider = assetProvider;
      _fruitFactory = fruitFactory;
      _windowService = windowService;
      _progressService = progressService;
    }

    public void Enter(string payload)
    {
      _windowService.CleanUp();
      _fruitFactory.CleanUp();
      _assetProvider.CleanUp();
      _progressService.Progress.NewTask();
      
      _sceneLoader.Load(payload);
    }
    
    public void Exit()
    {
    }
  }
}