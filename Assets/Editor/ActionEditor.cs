using System.Collections.Generic;
using Interaction.Actions;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Action), true)]
public class ActionEditor : IivimatEditor
{
    private SerializedProperty _groupTrigger;
    
    private SerializedProperty _specifyReactions;

    private SerializedProperty _reactionNames;

    protected override void LoadGui()
    {
        _groupTrigger = serializedObject.FindProperty("groupTrigger");
        _specifyReactions = serializedObject.FindProperty("specifyReactions");
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

        EditorGUILayout.Space();
        var specifyReactionsLabel = new GUIContent("Specify reactions",
            "If enabled, allows you to specify which reactions this action will trigger."
        );
        EditorGUILayout.PropertyField(_specifyReactions, specifyReactionsLabel);

        if (action.specifyReactions)
        {
            ListEditor.Show(_reactionNames, typeof(string), "Reaction", "No reaction name specified!",
                "Some reaction names are empty!");
        }
    }

    protected override IEnumerable<string> GetIgnoredFields()
    {
        return new[] {"groupTrigger", "specifyReactions", "reactionNames"};
    }
}