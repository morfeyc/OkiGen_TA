using System;
using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.Services.Inputs
{
  public interface IInputService
  {
    GameObject SelectedFruit { get; }
    UnityEvent OnSelectedFruitChanged { get; set; }
  }
}