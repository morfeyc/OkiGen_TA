using UnityEngine;

namespace CodeBase.Logic
{
  public class ConveyorMoveObjects : MonoBehaviour
  {
    [SerializeField] private Rigidbody Rigidbody;
    [SerializeField] private float Speed = 1;
    
    private void FixedUpdate()
    {
      Vector3 startPos = Rigidbody.position;
      Rigidbody.position -= transform.forward * (Speed * Time.fixedDeltaTime); // teleport back
      Rigidbody.MovePosition(startPos); // move forward
    }
  }
}