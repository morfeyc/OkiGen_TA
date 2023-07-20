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
    
    private readonly List<WindowBase> _opened = new();

    public WindowService(IUIFactory uiFactory)
    {
      _uiFactory = uiFactory;
    }
    
    public async UniTask Open(WindowId id)
    {
      WindowBase window = await _uiFactory.CreateWindow(id);
      window.Open();
      _opened.Add(window);
    }

    public void CleanUp()
    {
      foreach (WindowBase window in _opened.Where(window => window != null))
      {
        window.Close();
      }
    }
  }
}