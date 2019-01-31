using System.Collections.Generic;
using Interaction.Reactions;
using UnityEditor;
using UnityEngine;
using Action = Interaction.Actions.Action;

[CustomEditor(typeof(Action), true)]
public class ActionEditor : IivimatEditor
{
    private SerializedProperty _actionName;

    private SerializedProperty _groupTrigger;

    private SerializedProperty _triggerOtherObject;

    private SerializedProperty _objectToTrigger;

    private SerializedProperty _specifyReactions;

    protected override void LoadGui()
    {
        _actionName = serializedObject.FindProperty("actionName");
        _groupTrigger = serializedObject.FindProperty("groupTrigger");
        _triggerOtherObject = serializedObject.FindProperty("triggerOtherObject");
        _objectToTrigger = serializedObject.FindProperty("objectToTrigger");
        _specifyReactions = serializedObject.FindProperty("specifyReactions");
    }

    protected override void DrawGui()
    {
        var action = (Action) target;
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(_actionName);

        EditorGUILayout.Space();
        if (action.transform.childCount == 0)
            GUI.enabled = false;
        EditorGUILayout.PropertyField(_groupTrigger);
        GUI.enabled = true;
        
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(_triggerOtherObject);
        if (action.triggerOtherObject)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(_objectToTrigger);
            EditorGUI.indentLevel--;
        }

        EditorGUILayout.Space();
        var reactions = action.GetTargetedReactions();

        if (reactions.Count <= 1)
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
            ShowReactions(action, reactions);

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
        return new[]
            {"actionName", "triggerOtherObject", "objectToTrigger", "groupTrigger", "specifyReactions", "reactions"};
    }
}