using System.Collections.Generic;
using Interaction.Actions;
using Interaction.Reactions;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Action), true)]
public class ActionEditor : IivimatEditor
{
    private SerializedProperty _groupTrigger;

    private SerializedProperty _specifyReactions;

    private bool _firstTimeSpecifyingReaction = true;

    protected override void LoadGui()
    {
        _groupTrigger = serializedObject.FindProperty("groupTrigger");
        _specifyReactions = serializedObject.FindProperty("specifyReactions");
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
        var reactions = action.groupTrigger ? action.GetComponentsInChildren<Reaction>() : action.GetComponents<Reaction>();
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

        {
            if (_firstTimeSpecifyingReaction)
            {
                action.reactions = new List<Reaction>(reactions);
                _firstTimeSpecifyingReaction = false;
            }

            ShowReactions(action, reactions);
        }
    }

    private static void ShowReactions(Action action, IEnumerable<Reaction> reactions)
    {
        EditorGUI.indentLevel++;
        var counters = new Dictionary<string, int>();
        foreach (var reaction in reactions)
        {
            EditorGUILayout.BeginHorizontal();
            var reactionName = reaction.GetType().Name;
            if (counters.ContainsKey(reactionName))
                counters[reactionName]++;
            else
                counters[reactionName] = 1;
            EditorGUILayout.LabelField(reactionName + " " + counters[reactionName]);
            if (GUILayout.Toggle(action.reactions.Contains(reaction), new GUIContent(""), GUILayout.Width(12f)))
            {
                if (!action.reactions.Contains(reaction))
                    action.reactions.Add(reaction);
            }
            else
            {
                if (action.reactions.Contains(reaction))
                    action.reactions.Remove(reaction);
            }

            EditorGUILayout.EndHorizontal();
        }

        EditorGUI.indentLevel--;
    }

    protected override IEnumerable<string> GetIgnoredFields()
    {
        return new[] {"groupTrigger", "specifyReactions", "reactions"};
    }
}