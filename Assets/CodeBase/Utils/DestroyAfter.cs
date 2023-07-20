using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Utils
{
  public class DestroyAfter : MonoBehaviour
  {
    [SerializeField] private float Seconds;
    
    private bool _canceled;

    private void Awake()
    {
      StartTimer().Forget();
    }

    public void Cancel() => 
      _canceled = true;

    private async UniTask StartTimer()
    {
      await UniTask.Delay(TimeSpan.FromSeconds(Seconds), cancellationToken: this.GetCancellationTokenOnDestroy());
      
      if (!_canceled) 
        Destroy(gameObject);
    }
  }
}