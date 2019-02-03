using System.Collections.Generic;
using Interaction.Actions.Meta;
using Interaction.Reactions;
using UnityEditor;
using UnityEngine;
using Action = Interaction.Actions.Action;

[CustomEditor(typeof(PropagatedAction), true)]
public class PropagatedActionEditor : ActionEditor
{
    private SerializedProperty _triggerOnlyInRange;
    
    private SerializedProperty _triggerDistance;

    protected override void LoadGui()
    {
        base.LoadGui();
        _triggerOnlyInRange = serializedObject.FindProperty("triggerOnlyInRange");
        _triggerDistance = serializedObject.FindProperty("triggerDistance");
    }

    protected override void DrawGui()
    {
        base.DrawGui();
        var action = (PropagatedAction) target;
        EditorGUILayout.PropertyField(_triggerOnlyInRange);
        if (action.triggerOnlyInRange)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(_triggerDistance);
            EditorGUI.indentLevel--;
        }
    }

    protected override IEnumerable<string> GetIgnoredFields()
    {
        var ignoredFields = new List<string> {"triggerOnlyInRange", "triggerDistance"};
        ignoredFields.AddRange(base.GetIgnoredFields());
        return ignoredFields;
    }
}