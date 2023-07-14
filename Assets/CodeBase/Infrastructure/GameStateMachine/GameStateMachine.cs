using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Infrastructure.GameStateMachine.States;
using Zenject;

namespace CodeBase.Infrastructure.GameStateMachine
{
  public class GameStateMachine : IGameStateMachine, ITickable
  {
    private readonly Dictionary<Type, IExitableState> _states;
    private IExitableState _activeState;
    private ITickableState _tickableState;

    public GameStateMachine(IExitableState[] states)
    {
      _states = states
        .ToDictionary(s=> s.GetType(), s => s);
    }
    
    public void Enter<TState>() where TState : class, IState
    {
      IState state = ChangeState<TState>();
      state.Enter();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
    {
      TState state = ChangeState<TState>();
      state.Enter(payload);
    }

    public void Tick()
    {
      _tickableState?.Tick();
    }

    private TState ChangeState<TState>() where TState : class, IExitableState
    {
      _activeState?.Exit();
      
      TState state = GetState<TState>();
      _activeState = state;

      _tickableState = null;
      if (_activeState is ITickableState tickableState)
        _tickableState = tickableState;
      
      return state;
    }

    private TState GetState<TState>() where TState : class, IExitableState => 
      _states[typeof(TState)] as TState;
  }
}