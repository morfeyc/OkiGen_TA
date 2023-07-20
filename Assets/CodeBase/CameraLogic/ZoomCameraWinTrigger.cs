using CodeBase.Infrastructure.Services.PersistentProgress;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.CameraLogic
{
  public class ZoomCameraWinTrigger : MonoBehaviour
  {
    private ICameraService _cameraService;
    private IPersistentProgressService _progressService;

    [Inject]
    public void Construct(IPersistentProgressService progressService, ICameraService cameraService)
    {
      _progressService = progressService;
      _cameraService = cameraService;
    }

    private void Awake()
    {
      _progressService.Progress.Task.OnCompleted.AddListener(Zoom);
    }

    private void OnDestroy()
    {
      _progressService.Progress.Task.OnCompleted.RemoveListener(Zoom);
    }

    private async void Zoom()
    {
      for (int i = 60; i >= 30; i--)
      {
        _cameraService.Camera.m_Lens.FieldOfView = i;
        await UniTask.Yield();
      }
    }
  }
}