using UnityEngine;

namespace CodeBase.PlayerLogic
{
  public enum PlayerAnimation
  {
    Idle,
    Grab,
    Release,
    Dance
  }
  
  public class PlayerAnimator : MonoBehaviour
  {
    [SerializeField] private Animator Animator;
    
    private const string Idle = "Idle";
    private const string Grab = "Grab";
    private const string Dance = "Dance";

    public void SetState(PlayerAnimation state)
    {
      switch (state)
      {
        case PlayerAnimation.Idle:
          Animator.SetTrigger(Idle);
          break;
        case PlayerAnimation.Grab:
          Animator.SetBool(Grab, true);
          break;
        case PlayerAnimation.Release:
          Animator.SetBool(Grab, false);
          break;
        case PlayerAnimation.Dance:
          Animator.SetTrigger(Dance);
          break;
      }
    }
  }
}