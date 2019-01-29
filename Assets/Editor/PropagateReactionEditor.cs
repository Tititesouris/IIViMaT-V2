using System.Collections.Generic;
using System.Linq;
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

    protected override void LoadGui()
    {
        base.LoadGui();
        _targets = serializedObject.FindProperty("targets");
        _triggerSpecific = serializedObject.FindProperty("triggerSpecific");
        _nbPropagations = serializedObject.FindProperty("nbPropagations");
        _randomPropagation = serializedObject.FindProperty("randomPropagation");
        _removeTriggeredTargets = serializedObject.FindProperty("removeTriggeredTargets");
        _specifyActions = serializedObject.FindProperty("specifyActions");
    }

    protected override void DrawGui()
    {
        base.DrawGui();
        var propagateReaction = (PropagateReaction) target;

        ListEditor.Show(_targets, typeof(GameObject), "Target", "No target specified!",
            "Some targets are not specified!");

        EditorGUIUtility.labelWidth = 150;
        var triggerSpecificLabel = new GUIContent("Trigger only some targets",
            "If enabled, only a set number of targets will be triggered. If disabled, all targets will be triggered."
        );
        EditorGUILayout.PropertyField(_triggerSpecific, triggerSpecificLabel);

        if (propagateReaction.triggerSpecific)
        {
            EditorGUIUtility.labelWidth = 180;
            EditorGUI.indentLevel++;
            var nbPropagationsLabel = new GUIContent("Number of triggered targets",
                "Only this many targets will be triggered."
            );
            EditorGUILayout.PropertyField(_nbPropagations, nbPropagationsLabel);

            var randomPropagationLabel = new GUIContent("Random targets",
                "If enabled, " + propagateReaction.nbPropagations + " random targets will be triggered.\n" +
                "If disabled, the first " + propagateReaction.nbPropagations + " targets will be triggered."
            );
            EditorGUILayout.PropertyField(_randomPropagation, randomPropagationLabel);

            EditorGUILayout.PropertyField(_removeTriggeredTargets);
            EditorGUI.indentLevel--;
        }

        EditorGUILayout.Space();

        EditorGUIUtility.labelWidth = 120;

        var targetsActions = new Dictionary<GameObject, PropagatedAction[]>();
        foreach (var t in propagateReaction.targets)
        {
            if (t != null)
                targetsActions[t] = t.GetComponents<PropagatedAction>();
        }

        EditorGUILayout.PropertyField(_specifyActions);
        if (propagateReaction.specifyActions)
            ShowActions(propagateReaction, targetsActions);
    }

    private static void ShowActions(PropagateReaction propagateReaction,
        Dictionary<GameObject, PropagatedAction[]> targetsActions)
    {
        EditorGUI.indentLevel++;

        var specifiedActions = propagateReaction.GetSpecifiedActions();
        if (targetsActions.Values.Sum(actions => actions.Length) == 0)
            EditorGUILayout.HelpBox("No PropagatedAction on targets!", MessageType.Warning);
        else if (specifiedActions.Count == 0)
            EditorGUILayout.HelpBox("No action specified!", MessageType.Error);
        foreach (var targetAction in targetsActions)
        {
            EditorGUILayout.BeginHorizontal();
            var targetName = targetAction.Key.name;
            EditorGUILayout.LabelField(targetName);
            EditorGUILayout.EndHorizontal();

            EditorGUI.indentLevel++;

            foreach (var action in targetAction.Value)
            {
                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField(action.actionName);
                if (GUILayout.Toggle(specifiedActions.Contains(action), new GUIContent(""),
                    GUILayout.Width(12f)))
                {
                    if (!specifiedActions.Contains(action))
                        specifiedActions.Add(action);
                }
                else
                {
                    if (specifiedActions.Contains(action))
                        specifiedActions.Remove(action);
                }

                EditorGUILayout.EndHorizontal();
            }

            EditorGUI.indentLevel--;
        }

        propagateReaction.SetSpecifiedActions(specifiedActions);

        EditorGUI.indentLevel--;
    }

    protected override IEnumerable<string> GetIgnoredFields()
    {
        var ignoredFields = new List<string>
        {
            "targets", "triggerSpecific", "nbPropagations", "randomPropagation", "removeTriggeredTargets",
            "specifyActions", "actions"
        };
        ignoredFields.AddRange(base.GetIgnoredFields());
        return ignoredFields;
    }
}