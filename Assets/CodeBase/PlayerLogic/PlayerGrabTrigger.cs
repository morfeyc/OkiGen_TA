using System.Collections.Generic;
using System.Linq;
using CodeBase.Logic.Fruits;
using CodeBase.Services.Inputs;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace CodeBase.PlayerLogic
{
  public class PlayerGrabTrigger : MonoBehaviour
  {
    [SerializeField] private PlayerGrab PlayerGrab;
    [SerializeField] private float DistanceToGrab;

    private readonly List<GameObject> _grabbed = new();
    
    private IInputService _inputService;

    [Inject]
    public void Construct(IInputService inputService)
    {
      _inputService = inputService;
    }

    private void Update()
    {
      if(!PlayerGrab.CanGrab) 
        return;
      if (_inputService?.SelectedFruit == null) 
        return;
      if(!IsObjectToRight(_inputService.SelectedFruit.gameObject.transform.position)) 
        return;
      if(Vector3.Distance(transform.position, _inputService.SelectedFruit.transform.position) >= DistanceToGrab) 
        return;
      if(_grabbed.Any(obj => obj.Equals(_inputService.SelectedFruit))) 
        return;
      if(!_inputService.SelectedFruit.TryGetComponent(out FruitIdentifier fruitIdentifier)) 
        return;
      
      PlayerGrab.Grab(fruitIdentifier);
      _grabbed.Add(_inputService.SelectedFruit);
    }
    
    private bool IsObjectToRight(Vector3 targetPos)
    {
      Vector3 direction = targetPos - transform.position;
      Vector3 referenceRight = Vector3.Cross(transform.forward, Vector3.up);
      float dotProduct = Vector3.Dot(direction, referenceRight);
      return dotProduct > 0f;
    }
  }
}