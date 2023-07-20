using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic.Fruits;
using TMPro;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Windows.Main
{
  public class TaskText : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI Text;
    private IPersistentProgressService _progress;
    private string _startFormat;

    [Inject]
    public void Construct(IPersistentProgressService progress)
    {
      _progress = progress;
    }

    private void Awake()
    {
      _startFormat = Text.text;
    }

    public void UpdateComponent()
    {
      Init();
    }

    private void Init()
    {
      string fruitName = AllFruits.GetFruitName(_progress.Progress.Task.Id);
      int amount = _progress.Progress.Task.AmountToWin;
      
      Text.text = string.Format(_startFormat, amount.ToString(), fruitName);
    }
  }
}