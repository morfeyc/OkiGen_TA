using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI.Elements
{
  public class DestroyAfter : MonoBehaviour
  {
    [SerializeField] private float Seconds;

    private async void Awake()
    {
      await UniTask.Delay(TimeSpan.FromSeconds(Seconds));
      Destroy(gameObject);
    }
  }
}