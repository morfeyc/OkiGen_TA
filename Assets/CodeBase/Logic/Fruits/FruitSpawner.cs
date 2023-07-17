using System;
using System.Threading;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Factory.Fruit;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace CodeBase.Logic.Fruits
{
  public class FruitSpawner : MonoBehaviour
  {
    private readonly CancellationTokenSource _cancellationTokenSource = new();
    
    private float _spawnDelay;
    private IFruitFactory _fruitFactory;

    [Inject]
    public void ConstructDi(IFruitFactory fruitFactory)
    {
      _fruitFactory = fruitFactory;
    }

    public void Construct(float spawnDelay)
    {
      _spawnDelay = spawnDelay;
    }

    private void Start()
    {
      Spawn().Forget();
    }

    private async UniTask Spawn()
    {
      Array values = Enum.GetValues(typeof(FruitId));
      
      while (true)
      {
        _fruitFactory.CreateFruit(AllFruits.GetRandomFruitId(), transform.position);
        await UniTask.Delay(TimeSpan.FromSeconds(_spawnDelay), cancellationToken: _cancellationTokenSource.Token);
      }
    }

    private void OnDestroy()
    {
      _cancellationTokenSource.Cancel();
      _cancellationTokenSource.Dispose();
    }
  }
}