using CodeBase.Services.Inputs;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Fruits
{
  public class FruitSelectedOutline : MonoBehaviour
  {
    [SerializeField] private Material Selected;
    [SerializeField] private Material Unselected;
    
    private MeshRenderer _selected;
    private IInputService _inputService;

    [Inject]
    public void Construct(IInputService inputService)
    {
      _inputService = inputService;
    }

    private void Awake()
    {
      Init();
    }

    private void Init()
    {
      _inputService.OnSelectedFruitChanged.AddListener(ChangeOutline);
    }

    private void ChangeOutline()
    {
      if (_selected)
        _selected.material = Unselected;

      _selected = _inputService.SelectedFruit.GetComponent<MeshRenderer>();
      _selected.material = Selected;
    }
  }
}