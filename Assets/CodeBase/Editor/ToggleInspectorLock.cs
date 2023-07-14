using UnityEditor;

namespace CodeBase.Editor
{
  public class ToggleInspectorLock
  {
    static ToggleInspectorLock()
    {
      [MenuItem("Tools/Toggle Inspector Lock %SPACE")]
      static void ToggleAction()
      {
        ActiveEditorTracker.sharedTracker.isLocked = !ActiveEditorTracker.sharedTracker.isLocked;
        ActiveEditorTracker.sharedTracker.ForceRebuild();
      }
    }
  }
}