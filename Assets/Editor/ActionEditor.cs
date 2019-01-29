using System.Collections.Generic;
using Interaction.Reactions;
using UnityEditor;
using UnityEngine;
using Action = Interaction.Actions.Action;

[CustomEditor(typeof(Action), true)]
public class ActionEditor : IivimatEditor
{
    private SerializedProperty _actionName;

    private SerializedProperty _specifyTarget;

    private SerializedProperty _targets;

    private SerializedProperty _groupTrigger;

    private SerializedProperty _specifyReactions;

    protected override void LoadGui()
    {
        _actionName = serializedObject.FindProperty("actionName");
        _specifyTarget = serializedObject.FindProperty("specifyTarget");
        _targets = serializedObject.FindProperty("target");
        _groupTrigger = serializedObject.FindProperty("groupTrigger");
        _specifyReactions = serializedObject.FindProperty("specifyReactions");
    }

    protected override void DrawGui()
    {
        var action = (Action) target;
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(_actionName);

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(_specifyTarget);
        if (action.specifyTarget)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(_targets);
            EditorGUI.indentLevel--;
        }

        EditorGUILayout.Space();

        if (action.transform.childCount == 0)
            GUI.enabled = false;
        EditorGUILayout.PropertyField(_groupTrigger);
        GUI.enabled = true;

        EditorGUILayout.Space();
        var reactions = action.groupTrigger
            ? action.GetComponentsInChildren<Reaction>()
            : action.GetComponents<Reaction>();
        
        if (reactions.Length <= 1)
        {
            GUI.enabled = false;
            action.specifyReactions = false;
        }

        var specifyReactionsLabel = new GUIContent("Specify reactions",
            "If enabled, allows you to specify which reactions this action will trigger."
        );
        EditorGUILayout.PropertyField(_specifyReactions, specifyReactionsLabel);

        GUI.enabled = true;

        if (action.specifyReactions)
            if (action.specifyTarget)
            {
                ShowReactions(action, action.target.GetComponents<Reaction>());
            }
            else
            {
                ShowReactions(action, reactions);
            }
        EditorGUILayout.Space();
    }

    private static void ShowReactions(Action action, IEnumerable<Reaction> reactions)
    {
        EditorGUI.indentLevel++;
        var specifiedReactions = action.GetSpecifiedReactions();
        if (specifiedReactions.Count == 0)
            EditorGUILayout.HelpBox("No reaction specified!", MessageType.Error);
        foreach (var reaction in reactions)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(reaction.reactionName);
            if (GUILayout.Toggle(specifiedReactions.Contains(reaction), new GUIContent(""), GUILayout.Width(12f)))
            {
                if (!specifiedReactions.Contains(reaction))
                    specifiedReactions.Add(reaction);
            }
            else
            {
                if (specifiedReactions.Contains(reaction))
                    specifiedReactions.Remove(reaction);
            }

            EditorGUILayout.EndHorizontal();
        }
        action.SetSpecifiedReaction(specifiedReactions);

        EditorGUI.indentLevel--;
    }

    protected override IEnumerable<string> GetIgnoredFields()
    {
        return new[] {"actionName", "groupTrigger", "specifyReactions", "reactions", "target", "specifyTarget"};
    }
}