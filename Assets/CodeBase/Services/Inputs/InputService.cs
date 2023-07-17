using System;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace CodeBase.Services.Inputs
{
  public class InputService : IInputService, ITickable
  {
    public GameObject SelectedFruit { get; private set; }
    public UnityEvent OnSelectedFruitChanged { get; set; } = new();


    public void Tick()
    {
      UpdateFruitToCollect();
    }

    private void UpdateFruitToCollect()
    {
      if (!Input.GetMouseButtonDown(0)) return;

      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      if (!Physics.Raycast(ray, out RaycastHit hit, 100.0f)) return;
      if (!hit.transform.CompareTag("Fruit")) return;

      SelectedFruit = hit.transform.gameObject;
      OnSelectedFruitChanged?.Invoke();
    }
  }
}