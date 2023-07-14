using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Windows;

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
    
    public async Task Open(WindowId id)
    {
      if (TryOpenFromCache(id)) return;

      WindowBase window = await _uiFactory.CreateWindow(id);
      window.Open();
      _cachedWindows[id] = window;
    }

    private bool TryOpenFromCache(WindowId id)
    {
      if (!_cachedWindows.TryGetValue(id, out WindowBase window)) return false;
      
      window.Open();
      return true;
    }
  }
}