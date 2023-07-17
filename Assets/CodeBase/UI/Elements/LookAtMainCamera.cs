using UnityEngine;

namespace CodeBase.UI.Elements
{
  public class LookAtMainCamera : MonoBehaviour
  {
    private Camera _mainCamera;
    
    private void Awake()
    {
      _mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
      Rotate();
    }

    [ContextMenu("Rotate")]
    private void Rotate()
    {
      if(!_mainCamera) return;
      
      Quaternion rotation = _mainCamera.transform.rotation;
      transform.LookAt(transform.position + rotation * Vector3.back, rotation * Vector3.up);
      transform.Rotate(0,180,0);
    }
  }
}