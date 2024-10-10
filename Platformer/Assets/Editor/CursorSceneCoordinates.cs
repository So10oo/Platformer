using UnityEditor;
using UnityEngine;

public class CursorSceneCoordinates : EditorWindow
{
    private Vector2 _scenePosition;

    [MenuItem("Window/CursorSceneCoordinates")]
    static void Init() 
    {
        CursorSceneCoordinates window = GetWindow<CursorSceneCoordinates>();
        window.Show();
    }

    private void OnEnable() { SceneView.duringSceneGui += SceneViewDuring; }

    private void OnDisable() { SceneView.duringSceneGui -= SceneViewDuring; }

    private void SceneViewDuring(SceneView scene)
    {
        var e = Event.current;
        if (e.type == EventType.KeyDown && e.keyCode == KeyCode.Space)
        {
            float pixelsPerPoint = EditorGUIUtility.pixelsPerPoint;
            Vector2 mouse = e.mousePosition;
            mouse.x *= pixelsPerPoint;
            mouse.y = scene.camera.pixelHeight - mouse.y * pixelsPerPoint;
            _scenePosition = scene.camera.ScreenToWorldPoint(mouse);

            Repaint();
        }
    }

    void OnGUI()
    {
        EditorGUILayout.LabelField("Scene: ", _scenePosition.ToString());
    }
}
