using System;

namespace CodeBase.Data
{
  [Serializable]
  public class PlayerProgress
  {
    public GameTask Task;
    
    public PlayerProgress()
    {
      Task = new GameTask();
    }

    public void NewTask()
    {
      Task = new GameTask();
    }
  }
}