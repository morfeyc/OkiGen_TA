using CodeBase.Logic.Fruits;
using CodeBase.Services.PersistentProgress;
using CodeBase.UI.Services.Factory;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.PlayerLogic
{
  public class PlayerGrab : MonoBehaviour
  {
    public bool CanGrab => !Animation.IsGrabbing;
    
    [SerializeField] private Transform BasketPosition;
    [SerializeField] private PlayerGrabAnimation Animation;

    private IUIFactory _uiFactory;
    private IPersistentProgressService _progressService;

    [Inject]
    public void Construct(IUIFactory uiFactory, IPersistentProgressService progressService)
    {
      _uiFactory = uiFactory;
      _progressService = progressService;
    }

    public async UniTaskVoid Grab(FruitIdentifier fruit)
    {
      CancelDestroying(fruit);

      await Animation.Grab(fruit.gameObject.transform);
      
      if (IsProperFruit(fruit))
      {
        _uiFactory.CreateCollectPopup(BasketPosition.position);
        _progressService.Progress.Task.FruitCollect();
      }
    }

    private bool IsProperFruit(FruitIdentifier fruit)
    {
      return fruit.Id == _progressService.Progress.Task.Id;
    }

    private static void CancelDestroying(FruitIdentifier fruit)
    {
      FruitDestroy fruitDestroy = fruit.GetComponent<FruitDestroy>();
      fruitDestroy.Cancel();
    }
  }
}