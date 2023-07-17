using System;
using Random = UnityEngine.Random;

namespace CodeBase.Logic.Fruits
{
  public static class AllFruits
  {
    public static FruitId GetRandomFruitId()
    {
      Array values = Enum.GetValues(typeof(FruitId));
      return (FruitId)Random.Range(0, values.Length);
    }

    public static string GetFruitName(FruitId id)
    {
      int enumId = (int)id;
      return Enum.GetName(typeof(FruitId), enumId);
    }
  }
}