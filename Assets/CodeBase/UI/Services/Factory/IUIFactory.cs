using System.Threading.Tasks;
using CodeBase.UI.Services.Window;
using CodeBase.UI.Windows;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
  public interface IUIFactory
  {
    Task CreateUIRoot();
    Task<WindowBase> CreateWindow(WindowId id);
  }
}