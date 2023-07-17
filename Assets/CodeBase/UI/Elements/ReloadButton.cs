using System;
using CodeBase.Infrastructure.GameStateMachine.Provider;
using CodeBase.Infrastructure.GameStateMachine.States;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.Elements
{
  public class ReloadButton : MonoBehaviour
  {
    [SerializeField] private Button Button;
    private IGameStateMachineProvider _stateMachineProvider;

    [Inject]
    public void Construct(IGameStateMachineProvider stateMachineProvider)
    {
      _stateMachineProvider = stateMachineProvider;
    }

    private void Awake()
    {
      Button.onClick.AddListener(Reload);
    }

    private void OnDestroy()
    {
      Button.onClick.RemoveListener(Reload);
    }

    private void Reload()
    {
      _stateMachineProvider.Value.Enter<LoadLevelState, string>(SceneManager.GetActiveScene().name);
    }
  }
}