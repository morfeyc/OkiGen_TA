using System.Collections.Generic;
using CodeBase.Infrastructure.Services.Assets;
using CodeBase.Logic.Fruits;
using CodeBase.Services.StaticData;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace CodeBase.Infrastructure.Factory.Fruit
{
  public class FruitFactory : IFruitFactory, IInitializable
  {
    public List<FruitSpawner> Spawners { get; private set; } = new();

    private readonly IAssetProvider _assetProvider;
    private readonly IStaticDataService _staticData;
    
    private readonly List<GameObject> _spawnedFruits = new();
    private readonly DiContainer _diContainer;

    public FruitFactory(DiContainer diContainer, IAssetProvider assetProvider, IStaticDataService staticData)
    {
      _diContainer = diContainer;
      _assetProvider = assetProvider;
      _staticData = staticData;
    }

    public void Initialize()
    {
      _assetProvider.Load<GameObject>(AssetAddress.FruitSpawner);
    }

    public async void CreateFruit(FruitId id, Vector3 at)
    {
      if(!_staticData.Fruits.TryGetValue(id, out AssetReference fruitAssetReference)) throw new KeyNotFoundException();
      
      GameObject fruitPrefab = await _assetProvider.Load<GameObject>(fruitAssetReference);
      GameObject fruitObj = _diContainer.InstantiatePrefab(fruitPrefab, at, Quaternion.identity, null);
      _spawnedFruits.Add(fruitObj);
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
      foreach (GameObject fruitObj in _spawnedFruits)
        Object.Destroy(fruitObj);

      _spawnedFruits.Clear();
    }

    private void DestroySpawners()
    {
      foreach (FruitSpawner spawner in Spawners)
        Object.Destroy(spawner.gameObject);

      Spawners.Clear();
    }
  }
}