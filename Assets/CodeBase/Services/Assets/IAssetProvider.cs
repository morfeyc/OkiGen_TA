using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace CodeBase.Services.Assets
{
  public interface IAssetProvider
  {
    UniTask<T> Load<T>(AssetReference assetReference) where T : class;
    UniTask<T> Load<T>(string address) where T : class;
    UniTask<IList<T>> LoadMany<T>(string label) where T : class;
    void CleanUp();
  }
}