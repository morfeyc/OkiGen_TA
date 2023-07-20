using CodeBase.Infrastructure.GameStateMachine.Provider;
using CodeBase.Infrastructure.GameStateMachine.States;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace CodeBase.UI.Windows.TapToStart
{
  public class TapToStart : MonoBehaviour, IPointerClickHandler
  {
    [SerializeField] private WindowBase _window;
    private IGameStateMachineProvider _stateMachine;

    [Inject]
    public void Construct(IGameStateMachineProvider stateMachine)
    {
      _stateMachine = stateMachine;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
      _window.Close();
      _stateMachine.Value.Enter<GameLoopState>();
    }
  }
}