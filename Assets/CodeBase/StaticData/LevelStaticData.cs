using UnityEngine;

namespace CodeBase.StaticData
{
  [CreateAssetMenu(fileName = "LevelData", menuName = "Static Data/Level", order = 0)]
  public class LevelStaticData : ScriptableObject
  {
    public string LevelKey;
  }
}