using System;
using System.Collections.Generic;
using CodeBase.Logic.Fruits;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.StaticData
{
  [CreateAssetMenu(fileName = "FruitsStaticData", menuName = "Static Data/Fruits Static Data", order = 0)]
  public class FruitsStaticData : ScriptableObject
  {
    public List<FruitData> FruitsData;
  }

  [Serializable]
  public class FruitData
  {
    public FruitId Id;
    public AssetReference Asset;
  }
}