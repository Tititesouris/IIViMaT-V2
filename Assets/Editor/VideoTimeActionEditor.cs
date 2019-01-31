using System.Collections.Generic;
using Interaction.Actions.Video;
using UnityEditor;

[CustomEditor(typeof(VideoTimeAction), true)]
public class VideoTimeActionEditor : ActionEditor
{
    private SerializedProperty _startTime;
    private SerializedProperty _repeat;

    private SerializedProperty _interval;

    protected override void LoadGui()
    {
        base.LoadGui();
        _startTime = serializedObject.FindProperty("startTime");
        _repeat = serializedObject.FindProperty("repeat");
        _interval = serializedObject.FindProperty("interval");
    }

    protected override void DrawGui()
    {
        base.DrawGui();
        var action = (VideoTimeAction) target;
        EditorGUILayout.PropertyField(_startTime);
        EditorGUILayout.PropertyField(_repeat);
        if (action.repeat)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(_interval);
            EditorGUI.indentLevel--;
        }
    }

    protected override IEnumerable<string> GetIgnoredFields()
    {
        var ignoredFields = new List<string> {"startTime", "interval", "repeat"};
        ignoredFields.AddRange(base.GetIgnoredFields());
        return ignoredFields;
    }
}