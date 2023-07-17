using System.Collections.Generic;
using System.Linq;
using CodeBase.Logic.Fruits;
using CodeBase.Services.Assets;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Factory.Fruit
{
  public class FruitFactory : IFruitFactory, IInitializable
  {
    public List<FruitSpawner> Spawners { get; private set; } = new();
    private readonly List<GameObject> _fruits = new();

    private readonly IAssetProvider _assetProvider;
    private readonly DiContainer _diContainer;
    
    private Dictionary<FruitId, GameObject> _fruitsPrefabs;

    public FruitFactory(DiContainer diContainer, IAssetProvider assetProvider)
    {
      _diContainer = diContainer;
      _assetProvider = assetProvider;
    }

    public async void Initialize()
    {
      _fruitsPrefabs = (await _assetProvider.LoadMany<GameObject>(AssetAddress.FruitsLabelKey))
        .ToDictionary(f => f.GetComponent<FruitIdentifier>().Id, f => f);

      await _assetProvider.Load<GameObject>(AssetAddress.FruitSpawner);
    }

    public void CreateFruit(FruitId id, Vector3 at)
    {
      if (!_fruitsPrefabs.TryGetValue(id, out GameObject fruitPrefab)) return;

      GameObject fruitObj = _diContainer.InstantiatePrefab(fruitPrefab, at, Quaternion.identity, null);
      _fruits.Add(fruitObj);
    }

    public async void CreateFruitsSpawner(Vector3 at, float spawnDelay)
    {
      GameObject fruitsSpawnerPrefab = await _assetProvider.Load<GameObject>(AssetAddress.FruitSpawner);
      FruitSpawner fruitSpawner = _diContainer.InstantiatePrefabForComponent<FruitSpawner>(fruitsSpawnerPrefab, at, Quaternion.identity, null);
      
      fruitSpawner.enabled = false;
      fruitSpawner.Construct(spawnDelay);
      Spawners.Add(fruitSpawner);
    }

    public void CleanUp()
    {
      DestroySpawners();
      DestroyFruits();
    }

    private void DestroyFruits()
    {
      foreach (GameObject fruitObj in _fruits)
        Object.Destroy(fruitObj);

      _fruits.Clear();
    }

    private void DestroySpawners()
    {
      foreach (FruitSpawner spawner in Spawners)
        Object.Destroy(spawner.gameObject);

      Spawners.Clear();
    }
  }
}