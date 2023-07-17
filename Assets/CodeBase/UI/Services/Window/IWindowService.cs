using Cysharp.Threading.Tasks;

namespace CodeBase.UI.Services.Window
{
  public interface IWindowService
  {
    UniTask Open(WindowId id);
    void CleanUp();
  }
}