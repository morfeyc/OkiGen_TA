using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.Windows.Main
{
  public class ProgressSlider : MonoBehaviour
  {
    [SerializeField] private Slider Slider;
    private IPersistentProgressService _progress;

    [Inject]
    public void Construct(IPersistentProgressService progress)
    {
      _progress = progress;
    }

    public void UpdateComponent()
    {
      Init();
    }

    private void Init()
    {
      _progress.Progress.Task.OnFruitCollected.AddListener(OnFruitCollected);
      Slider.minValue = 0;
      Slider.maxValue = _progress.Progress.Task.AmountToWin;
      Slider.value = 0;
    }

    private void OnFruitCollected(int currentAmount)
    {
      Slider.value = currentAmount;
    }

    private void OnDestroy()
    {
      _progress.Progress.Task.OnFruitCollected.RemoveListener(OnFruitCollected);
    }
  }
}