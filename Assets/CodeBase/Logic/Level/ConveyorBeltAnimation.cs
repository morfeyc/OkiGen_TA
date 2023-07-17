using UnityEngine;

namespace CodeBase.Logic.Level
{
  public class ConveyorBeltAnimation : MonoBehaviour
  {
    [SerializeField] private MeshRenderer MeshRenderer;
    [SerializeField] private float Speed;
    
    private void Update()
    {
      MeshRenderer.material.mainTextureOffset += new Vector2(-1, 0) * (Speed * Time.deltaTime);
    }
  }
}