using UnityEngine;

namespace CodeBase.UI.Windows.Main
{
  public class MainWindow : WindowBase
  {
    [SerializeField] private ProgressSlider ProgressSlider;
    [SerializeField] private TaskText TaskText;

    public override void Open()
    {
      base.Open();
      ProgressSlider.UpdateComponent();
      TaskText.UpdateComponent();
    }
  }
}