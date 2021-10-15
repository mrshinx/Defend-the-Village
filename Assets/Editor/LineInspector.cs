using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Line))]
public class DrawBezierExample : Editor
{
    private void OnSceneViewGUI(SceneView sv)
    {
        Line be = target as Line;
        be.startPoint = Handles.PositionHandle(be.startPoint, Quaternion.identity);
        be.endPoint = Handles.PositionHandle(be.endPoint, Quaternion.identity);
        be.startTangent = Handles.PositionHandle(be.startTangent, Quaternion.identity);
        be.endTangent = Handles.PositionHandle(be.endTangent, Quaternion.identity);

        Handles.DrawBezier(be.startPoint, be.endPoint, be.startTangent, be.endTangent, Color.red, null, 2f);
      //  Handles.DrawBezier(be.transform.TransformPoint(be.startPoint), be.transform.TransformPoint(be.endPoint), be.transform.TransformPoint(be.startTangent), be.transform.TransformPoint(be.endTangent), Color.red, null, 2f);

    }

    void OnEnable()
    {
        SceneView.onSceneGUIDelegate += OnSceneViewGUI;
    }

    void OnDisable()
    {
        SceneView.onSceneGUIDelegate -= OnSceneViewGUI;
    }
}