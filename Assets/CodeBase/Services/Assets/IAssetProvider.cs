using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace CodeBase.Services.Assets
{
  public interface IAssetProvider
  {
    Task<T> Load<T>(AssetReference assetReference) where T : class;
    Task<T> Load<T>(string address) where T : class;
    void Cleanup();
    Task<IList<T>> LoadMany<T>(string label) where T : class;
  }
}