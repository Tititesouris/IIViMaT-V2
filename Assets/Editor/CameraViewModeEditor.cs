using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(CameraViewMode), true)]
public class CameraViewModeEditor : IivimatEditor
{
    private SerializedProperty _viewMode;
    
    private SerializedProperty _target;
    
    protected override void LoadGui()
    {
        _viewMode = serializedObject.FindProperty("viewMode");
        _target = serializedObject.FindProperty("target");
    }

    protected override void DrawGui()
    {
        var cameraViewMode = (CameraViewMode) target;
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(_viewMode);
        if (cameraViewMode.viewMode != CameraViewMode.ViewMode.FreeView)
        {
            EditorGUILayout.PropertyField(_target);
        }
    }

    protected override IEnumerable<string> GetIgnoredFields()
    {
        return new [] {"viewMode", "target"};
    }
}