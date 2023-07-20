using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.Services.SceneLoader
{
  public class SceneLoaderService : ISceneLoaderService
  {
    public void Load(string sceneName, Action onLoaded = null) => 
      LoadAsync(sceneName, onLoaded).Forget();

    private async UniTaskVoid LoadAsync(string sceneName, Action onLoaded = null)
    {
      AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync(sceneName);

      while(!loadSceneAsync.isDone)
        await UniTask.Yield();
      
      onLoaded?.Invoke();
    }
  }
}