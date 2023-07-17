using Cinemachine;
using CodeBase.CameraLogic;
using CodeBase.Infrastructure.Factory.Fruit;
using CodeBase.Logic.Fruits;
using CodeBase.PlayerLogic;
using CodeBase.UI.Services.Window;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Level
{
  public class LevelInstaller : MonoInstaller, IInitializable
  {
    [SerializeField] private GameObject PlayerPrefab;
    [SerializeField] private Transform PlayerSpawnPosition;

    [SerializeField] private CinemachineVirtualCamera CameraPrefab;
    [SerializeField] private Transform CameraSpawnPosition;
    
    [SerializeField] private FruitSpawnMarker[] FruitSpawnMarkers;
    [SerializeField] private FruitSelectedOutline Outliner;
    
    
    private IFruitFactory _fruitFactory;
    private IWindowService _windowService;
    private ICameraService _cameraService;

    [Inject]
    public void Construct(IFruitFactory fruitFactory, IWindowService windowService, ICameraService cameraService)
    {
      _fruitFactory = fruitFactory;
      _windowService = windowService;
      _cameraService = cameraService;
    }
    
    public override void InstallBindings()
    {
      Container
        .Bind<IInitializable>()
        .FromInstance(this);
      
      Container
        .BindInterfacesTo<LevelService>()
        .AsSingle()
        .NonLazy();
    }

    public void Initialize()
    {
      GameObject player = InitPlayer();
      InitCamera(lookAt: player.transform);
      InitSpawners();
      InitFruitOutliner();
      OpenTapToScreen();
    }

    private GameObject InitPlayer()
    {
      Container.Unbind<PlayerAnimator>();
      
      PlayerAnimator playerAnimator = Container
        .InstantiatePrefabForComponent<PlayerAnimator>(PlayerPrefab, 
          PlayerSpawnPosition.position, 
          PlayerSpawnPosition.rotation, 
          null);

      Container
        .Bind<PlayerAnimator>()
        .FromInstance(playerAnimator)
        .AsSingle();

      return playerAnimator.gameObject;
    }

    private void InitCamera(Transform lookAt)
    {
      CinemachineVirtualCamera camera = Container
        .InstantiatePrefabForComponent<CinemachineVirtualCamera>(CameraPrefab, 
          CameraSpawnPosition.position, 
          CameraSpawnPosition.rotation, 
          null);

      _cameraService.Camera = camera;
      camera.LookAt = lookAt;
    }

    private void InitSpawners()
    {
      foreach (FruitSpawnMarker spawnMarker in FruitSpawnMarkers)
      { 
        _fruitFactory.CreateFruitsSpawner(spawnMarker.transform.position, spawnMarker.SpawnDelay);
      }
    }

    private void InitFruitOutliner()
    {
      Container.InstantiatePrefab(Outliner);
    }

    private void OpenTapToScreen()
    {
      _windowService.Open(WindowId.TapToStart);
    }
  }
}