using System.Collections;
using System.Collections.Generic;
using Interaction.Actions.Clock;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ClockTimeAction), true)]
public class ClockTimeActionEditor : ActionEditor
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
        var action = (ClockTimeAction)target;
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