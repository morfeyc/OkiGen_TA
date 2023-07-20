using CodeBase.Infrastructure.Factory.Fruit;
using CodeBase.Logic.Fruits;
using CodeBase.UI.Services.Window;

namespace CodeBase.Infrastructure.GameStateMachine.States
{
  public class GameLoopState : IState
  {
    private readonly IWindowService _windowService;
    private readonly IFruitFactory _fruitFactory;

    public GameLoopState(IFruitFactory fruitFactory, IWindowService windowService)
    {
      _fruitFactory = fruitFactory;
      _windowService = windowService;
    }
    
    public void Enter()
    {
      _windowService.Open(WindowId.Main);
      EnableFruitSpawners();
    }

    public void Exit()
    {
    }

    private void EnableFruitSpawners()
    {
      foreach (FruitSpawner fruitSpawner in _fruitFactory.Spawners)
      {
        fruitSpawner.enabled = true;
      }
    }
  }
}