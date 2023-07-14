using CodeBase.UI.Windows;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.UI.Elements
{
  public class CloseWindowOnTap : MonoBehaviour, IPointerClickHandler
  {
    [SerializeField] private WindowBase _window;

    public void OnPointerClick(PointerEventData eventData) => 
      _window.Close();
  }
}