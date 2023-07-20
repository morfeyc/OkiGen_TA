using CodeBase.Data;
using CodeBase.Infrastructure.Factory.Fruit;
using CodeBase.Infrastructure.Services.Assets;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SceneLoader;

namespace CodeBase.Infrastructure.GameStateMachine.States
{
  public class LoadLevelState : IPayloadedState<string>
  {
    private readonly ISceneLoaderService _sceneLoader;
    private readonly IAssetProvider _assetProvider;
    private readonly IFruitFactory _fruitFactory;
    private readonly IPersistentProgressService _progressService;

    public LoadLevelState(ISceneLoaderService sceneLoader,
      IAssetProvider assetProvider,
      IFruitFactory fruitFactory,
      IPersistentProgressService progressService)
    {
      _sceneLoader = sceneLoader;
      _assetProvider = assetProvider;
      _fruitFactory = fruitFactory;
      _progressService = progressService;
    }

    public void Enter(string payload)
    {
      _fruitFactory.CleanUp();
      _assetProvider.CleanUp();
      InitProgress();

      _sceneLoader.Load(payload);
    }

    private void InitProgress()
    {
      _progressService.Progress = new PlayerProgress();
    }

    public void Exit()
    {
    }
  }
}