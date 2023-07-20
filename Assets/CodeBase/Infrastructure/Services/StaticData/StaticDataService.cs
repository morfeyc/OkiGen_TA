using System.Collections.Generic;
using System.Linq;
using CodeBase.Logic.Fruits;
using CodeBase.StaticData;
using CodeBase.UI.Services.Window;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace CodeBase.Infrastructure.Services.StaticData
{
  public class StaticDataService : IStaticDataService, IInitializable
  {
    private const string DefaultFolder = "Static Data";

    public Dictionary<WindowId, AssetReference> Windows { get; private set; }
    public Dictionary<FruitId, AssetReference> Fruits { get; private set; }

    public void Initialize()
    {
      Fruits = Resources.LoadAll<FruitsStaticData>(DefaultFolder)
        .First()
        .FruitsData
        .ToDictionary(f => f.Id, f => f.Asset);
      
      Windows = Resources.LoadAll<WindowsStaticData>(DefaultFolder)
        .First()
        .WindowsData
        .ToDictionary(f => f.Id, f => f.Asset);
      int a = 0;
    }
  }
}