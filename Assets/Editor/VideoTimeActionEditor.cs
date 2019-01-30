using System.Collections;
using System.Collections.Generic;
using Interaction.Actions;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(VideoTimeAction), true)]
public class VideoTimeActionEditor : ActionEditor
{
    private SerializedProperty _repeat;

    private SerializedProperty _interval;

    protected override void LoadGui()
    {
        base.LoadGui();
        _repeat = serializedObject.FindProperty("repeat");
        _interval = serializedObject.FindProperty("interval");
    }

    protected override void DrawGui()
    {
        base.DrawGui();
        var action = (VideoTimeAction)target;
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
        var ignoredFields = new List<string> { "interval", "repeat" };
        ignoredFields.AddRange(base.GetIgnoredFields());
        return ignoredFields;
    }
}
