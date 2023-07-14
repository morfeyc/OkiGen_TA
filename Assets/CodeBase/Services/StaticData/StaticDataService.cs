using System.Collections.Generic;
using System.Linq;
using CodeBase.StaticData;
using CodeBase.UI.Services.Window;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.StaticData
{
  public class StaticDataService : IStaticDataService, IInitializable
  {
    private const string LevelsDataLabel = "LevelsData";
    
    private Dictionary<string, LevelStaticData> _levels;
    private Dictionary<WindowId, GameObject> _windows;

    public void Initialize()
    {
      _levels = Resources
        .LoadAll<LevelStaticData>(LevelsDataLabel)
        .ToDictionary(x => x.LevelKey, x => x);
    }

    public LevelStaticData ForLevel(string sceneKey) =>
      _levels.TryGetValue(sceneKey, out LevelStaticData staticData)
        ? staticData
        : null;
  }
}