using CodeBase.Logic.Fruits;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
  [CustomEditor(typeof(FruitSpawnMarker))]
  public class FruitSpawnMarkerEditor : UnityEditor.Editor
  {
    [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
    public static void RenderCustomGizmo(FruitSpawnMarker spawner, GizmoType gizmo)
    {
      Gizmos.color = Color.white;
      Gizmos.DrawSphere(spawner.transform.position, 0.15f);
    }
  }
}