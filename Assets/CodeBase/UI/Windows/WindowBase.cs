using UnityEngine;

namespace CodeBase.UI.Windows
{
  [RequireComponent(typeof(Canvas))]
  public class WindowBase : MonoBehaviour
  {
    [SerializeField] private Canvas _canvas;

    private void Awake() =>
      OnAwake();

    public virtual void OnAwake()
    {
    }

    public virtual void Open() => 
      _canvas.enabled = true;

    public virtual void Close() =>
      _canvas.enabled = false;

    private void OnValidate() =>
      _canvas = GetComponent<Canvas>()
                ?? null;
  }
}