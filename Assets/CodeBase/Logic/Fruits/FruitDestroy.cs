using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Logic.Fruits
{
  public class FruitDestroy : MonoBehaviour
  {
    [SerializeField] public float DestroyTime;
    private bool _canceled;

    private void Awake()
    {
      WaitDestroy().Forget();
    }

    public void Cancel()
    {
      _canceled = true;
    }

    private async UniTaskVoid WaitDestroy()
    {
      await UniTask.Delay(TimeSpan.FromSeconds(DestroyTime), DelayType.Realtime, PlayerLoopTiming.Update, this.GetCancellationTokenOnDestroy());
      if (!_canceled)
      {
        Destroy(gameObject);
      }
    } 
  }
}