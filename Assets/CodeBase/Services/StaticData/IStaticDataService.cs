using CodeBase.StaticData;

namespace CodeBase.Services.StaticData
{
  public interface IStaticDataService
  {
    LevelStaticData ForLevel(string sceneKey);
  }
}