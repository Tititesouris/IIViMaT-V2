using System.Collections.Generic;
using System.Linq;
using Interaction.Actions;
using Interaction.Reactions;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Reaction), true)]
public class ReactionEditor : IivimatEditor
{
    private SerializedProperty _reactionName;

    private SerializedProperty _triggerTime;

    private SerializedProperty _delay;

    protected override void LoadGui()
    {
        _reactionName = serializedObject.FindProperty("reactionName");
        _triggerTime = serializedObject.FindProperty("triggerTime");
        _delay = serializedObject.FindProperty("delay");
    }

    protected override void DrawGui()
    {
        var reaction = (Reaction) target;
        EditorGUILayout.Space();

        var actions = Selection.activeGameObject.GetComponentsInParent<Action>();
        if (reaction.reactionName.Length == 0 && !actions.Any(action => action.specifyReactions))
            GUI.enabled = false;
        EditorGUILayout.PropertyField(_reactionName);
        GUI.enabled = true;
        EditorGUILayout.PropertyField(_triggerTime);
        EditorGUILayout.PropertyField(_delay);

        var triggerOnlyOnceLabel = new GUIContent("Trigger only once",
            "If enabled, the reaction can only be triggered once."
        );
        reaction.triggerOnlyOnce = EditorGUILayout.Toggle(triggerOnlyOnceLabel, reaction.triggerOnlyOnce);

        if (!reaction.triggerOnlyOnce)
        {
            EditorGUI.indentLevel++;
            var cooldownLabel = new GUIContent("Cooldown",
                "The minimum amount of time in seconds between two triggers."
            );
            reaction.cooldown = EditorGUILayout.FloatField(cooldownLabel, reaction.cooldown);
            
            var repeatLabel = new GUIContent("Repeat",
                "Select to repeat automatically, without an action trigger, after the cooldown."
            );
            reaction.repeat = (Reaction.RepeatOptions) EditorGUILayout.EnumPopup(repeatLabel, reaction.repeat);
            
            if (reaction.repeat == Reaction.RepeatOptions.Fixed)
            {
                EditorGUI.indentLevel++;
                var relativeHeadingLabel = new GUIContent("Number of repeats",
                    "The number of times the reaction will repeat.");
                reaction.nbRepeat = EditorGUILayout.IntField(relativeHeadingLabel, reaction.nbRepeat);
                EditorGUI.indentLevel--;
            }
            EditorGUI.indentLevel--;
        }
    }

    protected override IEnumerable<string> GetIgnoredFields()
    {
        return new [] {"reactionName", "triggerTime", "delay", "triggerOnlyOnce", "cooldown", "repeat", "nbRepeat"};
    }
}