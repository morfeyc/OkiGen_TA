using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeBase.Services.Assets;
using CodeBase.UI.Services.Window;
using CodeBase.UI.Windows;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Services.Factory
{
  public class UIFactory : IUIFactory, IInitializable
  {
    private const string UIRootKey = "UIRoot";
    private const string UIWindowsLabelKey = "UIWindow";

    private readonly DiContainer _diContainer;
    private readonly IAssetProvider _assets;

    private Transform _uiRoot;
    private Dictionary<WindowId, GameObject> _windowsPrefabs;

    public UIFactory(DiContainer diContainer, IAssetProvider assets)
    {
      _diContainer = diContainer;
      _assets = assets;
    }

    public async void Initialize()
    {
      await CreateUIRoot();
    }

    public async UniTask<WindowBase> CreateWindow(WindowId id)
    {
      if(_windowsPrefabs == null) await LoadWindowsPrefabs();
      
      _windowsPrefabs.TryGetValue(id, out GameObject windowObj);
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

    private async Task LoadWindowsPrefabs()
    {
      _windowsPrefabs = (await _assets.LoadMany<GameObject>(UIWindowsLabelKey))
        .ToDictionary(window => window.GetComponent<WindowIdentifier>().Id, w => w);
    }
  }
}