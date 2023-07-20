using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeBase.Infrastructure.GameStateMachine.States;
using CodeBase.Infrastructure.Services.Assets;
using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.UI.Services.Window;
using CodeBase.UI.Windows;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace CodeBase.UI.Services.Factory
{
  public class UIFactory : IUIFactory, IInitializable
  {
    private const string UIRootKey = "UIRoot";

    private readonly DiContainer _diContainer;
    private readonly IStaticDataService _staticData;
    private readonly IAssetProvider _assets;

    private Transform _uiRoot;

    public UIFactory(DiContainer diContainer, IStaticDataService staticData, IAssetProvider assets)
    {
      _diContainer = diContainer;
      _staticData = staticData;
      _assets = assets;
    }

    public async void Initialize()
    {
      await CreateUIRoot();
    }

    public async UniTask<WindowBase> CreateWindow(WindowId id)
    {
      if (!_staticData.Windows.TryGetValue(id, out AssetReference windowsAssetReference)) throw new KeyNotFoundException();
      
      GameObject windowObj = await _assets.Load<GameObject>(windowsAssetReference);
      return _diContainer.InstantiatePrefabForComponent<WindowBase>(windowObj, _uiRoot);
    }

    public async UniTask CreateCollectPopup(Vector3 at)
    {
      GameObject obj = await _assets.Load<GameObject>(AssetAddress.CollectPopup);
      _diContainer.InstantiatePrefab(obj, at, Quaternion.identity, null);
    }

    private async UniTask CreateUIRoot()
    {
      if(_uiRoot != null) return;
      GameObject uiRootPrefab = await _assets.Load<GameObject>(UIRootKey);
      GameObject uiRootObj = _diContainer.InstantiatePrefab(uiRootPrefab);
      _uiRoot = uiRootObj.transform;
    }
  }
}