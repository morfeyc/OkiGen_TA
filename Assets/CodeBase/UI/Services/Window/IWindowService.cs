using System.Threading.Tasks;

namespace CodeBase.UI.Services.Window
{
  public interface IWindowService
  {
    Task Open(WindowId id);
  }
}