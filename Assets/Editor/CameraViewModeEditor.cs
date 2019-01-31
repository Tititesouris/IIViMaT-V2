using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(SpectatorViewMode), true)]
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
        var cameraViewMode = (SpectatorViewMode) target;
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(_viewMode);
        if (cameraViewMode.viewMode != SpectatorViewMode.ViewMode.FreeView)
        {
            EditorGUILayout.PropertyField(_target);
        }
    }

    protected override IEnumerable<string> GetIgnoredFields()
    {
        return new [] {"viewMode", "target"};
    }
}