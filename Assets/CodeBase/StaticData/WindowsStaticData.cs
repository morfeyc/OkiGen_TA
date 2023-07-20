using System;
using System.Collections.Generic;
using CodeBase.UI.Services.Window;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.StaticData
{
  [CreateAssetMenu(fileName = "WindowsStaticData", menuName = "Static Data/Windows Static Data", order = 1)]
  public class WindowsStaticData : ScriptableObject
  {
    public List<WindowData> WindowsData;
  }

  [Serializable]
  public class WindowData
  {
    public WindowId Id;
    public AssetReference Asset;
  }
}