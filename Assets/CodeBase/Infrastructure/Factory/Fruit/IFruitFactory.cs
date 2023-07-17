using System.Collections.Generic;
using CodeBase.Logic.Fruits;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory.Fruit
{
  public interface IFruitFactory
  {
    List<FruitSpawner> Spawners { get; }
    
    void CreateFruit(FruitId id, Vector3 at);
    void CreateFruitsSpawner(Vector3 at, float spawnDelay);
    void CleanUp();
  }
}