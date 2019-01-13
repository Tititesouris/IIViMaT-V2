using System.Collections.Generic;
using Interaction.Actions;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Action), true)]
public class ActionEditor : IivimatEditor
{
    private SerializedProperty _groupTrigger;

    private SerializedProperty _reactionNames;

    protected override void LoadGui()
    {
        _groupTrigger = serializedObject.FindProperty("groupTrigger");
        _reactionNames = serializedObject.FindProperty("reactionNames");
    }

    protected override void DrawGui()
    {
        var action = (Action) target;
        EditorGUILayout.Space();
        if (action.transform.childCount == 0)
            GUI.enabled = false;
        EditorGUILayout.PropertyField(_groupTrigger);
        GUI.enabled = true;

        var specifyReactionsLabel = new GUIContent("Specify reactions",
            "If enabled, allows you to specify which reactions this action will trigger."
        );
        action.specifyReactions = EditorGUILayout.BeginToggleGroup(specifyReactionsLabel, action.specifyReactions);

        if (action.specifyReactions)
        {
            ListEditor.Show(_reactionNames, "Reaction", "No reaction name specified", "Some reaction names are empty");
        }

        EditorGUILayout.EndToggleGroup();
    }

    protected override IEnumerable<string> GetIgnoredFields()
    {
        return new[] {"groupTrigger", "specifyReactions", "reactionNames"};
    }
}