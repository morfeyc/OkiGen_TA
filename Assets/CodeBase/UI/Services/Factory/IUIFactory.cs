using CodeBase.UI.Services.Window;
using CodeBase.UI.Windows;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
  public interface IUIFactory
  {
    UniTask<WindowBase> CreateWindow(WindowId id);
    UniTask CreateCollectPopup(Vector3 at);
  }
}