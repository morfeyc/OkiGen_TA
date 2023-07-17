using System;
using CodeBase.Logic.Fruits;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace CodeBase.Data
{
  [Serializable]
  public class GameTask
  {
    public FruitId Id { get; private set; }
    public int AmountToWin { get; private set; }
    public int CurrentAmount { get; private set; }
    public bool Completed { get; private set; } = false;

    public UnityEvent<int> OnFruitCollected = new();
    public UnityEvent OnCompleted = new();

    public GameTask()
    {
      Id = AllFruits.GetRandomFruitId();
      AmountToWin = Random.Range(1, Constants.MaxFruitsToCollect);
    }

    public void FruitCollect()
    {
      CurrentAmount++;
      Completed = CurrentAmount >= AmountToWin;
      if(Completed) OnCompleted?.Invoke();
      OnFruitCollected?.Invoke(CurrentAmount);
    }
  }
}