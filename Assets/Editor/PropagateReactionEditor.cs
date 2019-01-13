using System.Collections.Generic;
using Interaction.Actions;
using Interaction.Reactions;
using Interaction.Reactions.Meta;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PropagateReaction), true)]
public class PropagateReactionEditor : ReactionEditor
{
    private SerializedProperty _targets;

    private SerializedProperty _triggerSpecific;

    private SerializedProperty _nbPropagations;

    private SerializedProperty _randomPropagation;

    private SerializedProperty _removeTriggeredTargets;

    private SerializedProperty _specifyActions;

    private SerializedProperty _actionNames;

    protected override void LoadGui()
    {
        base.LoadGui();
        _targets = serializedObject.FindProperty("targets");
        _triggerSpecific = serializedObject.FindProperty("triggerSpecific");
        _nbPropagations = serializedObject.FindProperty("nbPropagations");
        _randomPropagation = serializedObject.FindProperty("randomPropagation");
        _removeTriggeredTargets = serializedObject.FindProperty("removeTriggeredTargets");
        _specifyActions = serializedObject.FindProperty("specifyActions");
        _actionNames = serializedObject.FindProperty("actionNames");
    }

    protected override void DrawGui()
    {
        base.DrawGui();
        var reaction = (PropagateReaction) target;

        ListEditor.Show(_targets, typeof(GameObject), "Target", "No target specified!",
            "Some targets are not specified!");

        EditorGUIUtility.labelWidth = 150;
        var triggerSpecificLabel = new GUIContent("Trigger specific targets",
            "If enabled, only a set number of targets will be triggered. If disabled, all targets will be triggered."
        );
        EditorGUILayout.PropertyField(_triggerSpecific, triggerSpecificLabel);

        if (reaction.triggerSpecific)
        {
            EditorGUIUtility.labelWidth = 180;
            EditorGUI.indentLevel++;
            var nbPropagationsLabel = new GUIContent("Number of triggered targets",
                "Only this many targets will be triggered."
            );
            EditorGUILayout.PropertyField(_nbPropagations, nbPropagationsLabel);

            var randomPropagationLabel = new GUIContent("Random targets",
                "If enabled, " + reaction.nbPropagations + " random targets will be triggered.\n" +
                "If disabled, the first " + reaction.nbPropagations + " targets will be triggered."
            );
            EditorGUILayout.PropertyField(_randomPropagation, randomPropagationLabel);

            EditorGUILayout.PropertyField(_removeTriggeredTargets);
            EditorGUI.indentLevel--;
        }

        EditorGUILayout.Space();

        EditorGUIUtility.labelWidth = 120;
        EditorGUILayout.PropertyField(_specifyActions);
        if (reaction.specifyActions)
        {
            ListEditor.Show(_actionNames, typeof(string), "Action", "No action name specified!",
                "Some action names are not specified!");
        }
    }

    protected override IEnumerable<string> GetIgnoredFields()
    {
        var ignoredFields = new List<string>
        {
            "targets", "triggerSpecific", "nbPropagations", "randomPropagation", "removeTriggeredTargets",
            "specifyActions", "actionNames"
        };
        ignoredFields.AddRange(base.GetIgnoredFields());
        return ignoredFields;
    }
}