using System;
using System.Collections.Generic;
using CodeBase.CameraLogic;
using CodeBase.Infrastructure.Factory.Fruit;
using CodeBase.Infrastructure.GameStateMachine;
using CodeBase.Infrastructure.GameStateMachine.Provider;
using CodeBase.Infrastructure.GameStateMachine.States;
using CodeBase.Infrastructure.Services.Assets;
using CodeBase.Infrastructure.Services.Inputs;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SceneLoader;
using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Services.Window;
using Zenject;

namespace CodeBase.Infrastructure
{
  public class BootstrapInstaller : MonoInstaller, IInitializable
  {
    public override void InstallBindings()
    {
      BindStaticData();
      BindInputs();
      BindAssets();
      BindSceneLoader();
      BindServices();
      BindFactories();
      BindUIServices();
      BindGameStateMachine();
      BindSelf();
    }

    public void Initialize()
    {
      IGameStateMachine gameStateMachine = Container.Resolve<IGameStateMachine>();
      Container.Resolve<IGameStateMachineProvider>().Value = gameStateMachine;
      gameStateMachine.Enter<BootstrapState>();
    }

    private void BindStaticData()
    {
      Container
        .BindInterfacesTo<StaticDataService>()
        .AsSingle()
        .NonLazy();
    }

    private void BindInputs()
    {
      Container
        .BindInterfacesTo<InputService>()
        .AsSingle()
        .NonLazy();
    }

    private void BindAssets()
    {
      Container
        .BindInterfacesTo<AssetProvider>()
        .AsSingle()
        .NonLazy();
    }

    private void BindSceneLoader()
    {
      Container
        .BindInterfacesTo<SceneLoaderService>()
        .AsSingle()
        .NonLazy();
    }
    
    private void BindServices()
    {
      Container
        .BindInterfacesTo<PersistentProgressService>()
        .AsSingle()
        .NonLazy();
    }

    private void BindFactories()
    {
      Container
        .BindInterfacesTo<FruitFactory>()
        .AsSingle()
        .NonLazy();
    }

    private void BindUIServices()
    {
      Container
        .BindInterfacesTo<UIFactory>()
        .AsSingle()
        .NonLazy();
      
      Container
        .BindInterfacesTo<WindowService>()
        .AsSingle()
        .NonLazy();
      
      Container
        .BindInterfacesTo<CameraService>()
        .AsSingle()
        .NonLazy();
    }

    private void BindGameStateMachine()
    {
      Container
        .BindInterfacesTo<GameStateMachineProvider>()
        .AsSingle()
        .NonLazy();

      BindGameStateMachineStates();

      Container
        .BindInterfacesTo<GameStateMachine.GameStateMachine>()
        .AsSingle()
        .NonLazy();
    }

    private void BindGameStateMachineStates()
    {
      Container
        .BindInterfacesTo<BootstrapState>()
        .AsSingle()
        .NonLazy();
      
      Container
        .BindInterfacesTo<LoadLevelState>()
        .AsSingle()
        .NonLazy();

      Container
        .BindInterfacesTo<GameLoopState>()
        .AsSingle()
        .NonLazy();
    }

    private void BindSelf()
    {
      Container
        .Bind<IInitializable>()
        .FromInstance(this);
    }

    private void OnApplicationQuit()
    {
      List<IDisposable> disposables = Container.ResolveAll<IDisposable>();
      foreach (IDisposable disposable in disposables)
      {
        disposable.Dispose();
      }
    }
  }
}