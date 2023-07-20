using System;

namespace CodeBase.Infrastructure.Services.SceneLoader
{
  public interface ISceneLoaderService
  {
    void Load(string sceneName, Action onLoaded = null);
  }
}