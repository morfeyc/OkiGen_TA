using System;

namespace CodeBase.Services.SceneLoader
{
  public interface ISceneLoaderService
  {
    void Load(string sceneName, Action onLoaded = null);
  }
}