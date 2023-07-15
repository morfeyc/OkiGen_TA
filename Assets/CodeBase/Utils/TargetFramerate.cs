using UnityEngine;

namespace CodeBase.Utils
{
  public class TargetFramerate : MonoBehaviour
  {
    [SerializeField] private int FPSLimit = 60;
    
    private void Awake()
    {
      Application.targetFrameRate = FPSLimit;
    }
  }
}