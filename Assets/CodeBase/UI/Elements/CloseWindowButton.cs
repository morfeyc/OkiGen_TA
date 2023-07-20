using CodeBase.UI.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
  public class CloseWindowButton : MonoBehaviour
  {
    [SerializeField] private Button Button;
    [SerializeField] private WindowBase Window;

    private void Awake() => 
      Button.onClick.AddListener(CloseWindow);

    private void OnDestroy() => 
      Button.onClick.RemoveListener(CloseWindow);

    private void CloseWindow() => 
      Window.Close();

    private void Reset()
    {
      Button = GetComponent<Button>();
    }
  }
}