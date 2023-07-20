using System.Collections.Generic;
using System.Linq;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Windows;
using Cysharp.Threading.Tasks;

namespace CodeBase.UI.Services.Window
{
  public class WindowService : IWindowService
  {
    private readonly IUIFactory _uiFactory;

    private readonly Dictionary<WindowId, WindowBase> _cachedWindows = new();

    public WindowService(IUIFactory uiFactory)
    {
      _uiFactory = uiFactory;
    }

    public async UniTask Open(WindowId id)
    {
      if (TryOpenFromCache(id)) return;

      WindowBase window = await _uiFactory.CreateWindow(id);
      window.Open();
      _cachedWindows[id] = window;
    }

    public void CloseAll()
    {
      foreach (WindowBase window in _cachedWindows.Values) 
        window.Close();
    }

    private bool TryOpenFromCache(WindowId id)
    {
      if (!_cachedWindows.TryGetValue(id, out WindowBase window)) return false;

      window.Open();
      return true;
    }
  }
}