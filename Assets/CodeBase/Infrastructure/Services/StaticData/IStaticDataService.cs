using System.Collections.Generic;
using CodeBase.Logic.Fruits;
using CodeBase.UI.Services.Window;
using UnityEngine.AddressableAssets;

namespace CodeBase.Services.StaticData
{
  public interface IStaticDataService
  {
    Dictionary<WindowId, AssetReference> Windows { get; }
    Dictionary<FruitId, AssetReference> Fruits { get;  }
    void Initialize();
  }
}