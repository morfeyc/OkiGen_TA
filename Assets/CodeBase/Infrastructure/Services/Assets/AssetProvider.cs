using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace CodeBase.Infrastructure.Services.Assets
{
  public class AssetProvider : IAssetProvider, IInitializable, IDisposable
  {
    private readonly Dictionary<string, List<AsyncOperationHandle>> _handles = new();

    public async void Initialize() =>
      await Addressables.InitializeAsync().Task;

    public async UniTask<T> Load<T>(AssetReference assetReference) where T : class
    {
      if (TryToGetCached(assetReference.AssetGUID, out AsyncOperationHandle completedHandle))
        return completedHandle.Result as T;

      return await RunWithCacheOnComplete(Addressables.LoadAssetAsync<T>(assetReference), assetReference.AssetGUID);
    }

    public async UniTask<T> Load<T>(string address) where T : class
    {
      if (TryToGetCached(address, out AsyncOperationHandle completedHandle))
        return completedHandle.Result as T;

      return await RunWithCacheOnComplete(Addressables.LoadAssetAsync<T>(address), address);
    }

    public void Dispose()
    {
      foreach (List<AsyncOperationHandle> resourceHandles in _handles.Values)
      foreach (AsyncOperationHandle handle in resourceHandles)
        Addressables.Release(handle);

      _handles.Clear();
    }

    private bool TryToGetCached(string key, out AsyncOperationHandle completeHandle)
    {
      if (_handles.TryGetValue(key, out List<AsyncOperationHandle> handlesList))
      {
        foreach (AsyncOperationHandle handle in handlesList)
        {
          if (handle.Status == AsyncOperationStatus.Succeeded)
          {
            completeHandle = handle;
            return true;
          }
        }
      }

      completeHandle = default;
      return false;
    }

    private async UniTask<T> RunWithCacheOnComplete<T>(AsyncOperationHandle<T> handle, string cacheKey) where T : class
    {
      if (!_handles.TryGetValue(cacheKey, out List<AsyncOperationHandle> resourceHandle))
      {
        resourceHandle = new List<AsyncOperationHandle>();
        _handles[cacheKey] = resourceHandle;
      }

      resourceHandle.Add(handle);
      return await handle.Task;
    }
  }
}