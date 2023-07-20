using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.Infrastructure.Services.Inputs
{
  public interface IInputService
  {
    GameObject SelectedFruit { get; }
    UnityEvent OnSelectedFruitChanged { get; set; }
  }
}