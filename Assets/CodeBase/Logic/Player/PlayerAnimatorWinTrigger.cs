using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Player
{
  public class PlayerAnimatorWinTrigger : MonoBehaviour
  {
    [SerializeField] private PlayerAnimator PlayerAnimator;
    private IPersistentProgressService _progressService;

    [Inject]
    public void Construct(IPersistentProgressService progressService)
    {
      _progressService = progressService;
    }

    private void Awake()
    {
      _progressService?.Progress.Task.OnCompleted.AddListener(StartDance);
    }

    private void StartDance()
    {
      PlayerAnimator.SetState(PlayerAnimation.Dance);
    }

    private void OnDestroy()
    {
      _progressService?.Progress.Task.OnCompleted.RemoveListener(StartDance);
    }
  }
}