using CodeBase.Services.Assets;
using Zenject;

namespace CodeBase.Infrastructure.Factory
{
  public class GameFactory : IGameFactory, IInitializable
  {
    private readonly DiContainer _diContainer;
    private readonly IAssetProvider _assets;

    public GameFactory(DiContainer diContainer, IAssetProvider assets)
    {
      _diContainer = diContainer;
      _assets = assets;
    }

    public void Initialize()
    {
      // await _assets.Load<GameObject>(AssetAddress.Example); // TODO: delete this line
    }

    public void Cleanup()
    {
      
    }
  }
}