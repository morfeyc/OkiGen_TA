using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeBase.Services.Assets;
using CodeBase.UI.Services.Window;
using CodeBase.UI.Windows;
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
    private Dictionary<WindowId, GameObject> _windows;

    public UIFactory(DiContainer diContainer, IAssetProvider assets)
    {
      _diContainer = diContainer;
      _assets = assets;
    }

    public async void Initialize()
    {
      _windows = (await _assets.LoadMany<GameObject>(UIWindowsLabelKey))
        .ToDictionary(window => window.GetComponent<WindowIdentifier>().Id, w => w);
    }

    public async Task CreateUIRoot()
    {
      GameObject uiRootPrefab = await _assets.Load<GameObject>(UIRootKey);
      GameObject uiRootObj = _diContainer.InstantiatePrefab(uiRootPrefab);
      _uiRoot = uiRootObj.transform;
    }

    public async Task<WindowBase> CreateWindow(WindowId id)
    {
      _windows.TryGetValue(id, out GameObject windowObj);
      return _diContainer.InstantiatePrefabForComponent<WindowBase>(windowObj, _uiRoot);
    }
  }
}