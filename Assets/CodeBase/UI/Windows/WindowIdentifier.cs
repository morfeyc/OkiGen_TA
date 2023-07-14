using CodeBase.UI.Services.Window;
using UnityEngine;

namespace CodeBase.UI.Windows
{
  public class WindowIdentifier : MonoBehaviour
  {
    [field: SerializeField] public WindowId Id { get; private set; } 
  }
}