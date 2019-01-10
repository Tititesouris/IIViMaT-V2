using System.Linq;
using Interaction.Actions;
using Interaction.Reactions;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Reaction), true)]
public class ReactionEditor : Editor
{
    private static readonly string[] ExcludedProperties =
        {"m_Script", "reactionName", "triggerTime", "delay", "triggerOnlyOnce", "cooldown"};


    private SerializedProperty _reactionName;
    private SerializedProperty _triggerTime;
    private SerializedProperty _delay;

    private void OnEnable()
    {
        _reactionName = serializedObject.FindProperty("reactionName");
        _triggerTime = serializedObject.FindProperty("triggerTime");
        _delay = serializedObject.FindProperty("delay");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        var reaction = (Reaction) target;

        EditorGUILayout.Space();

        var actions = Selection.activeGameObject.GetComponents<Action>();
        if (!actions.Any(action => action.specifyReactions))
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
            var cooldownLabel = new GUIContent("Cooldown",
                "The minimum amount of time in seconds between two triggers."
            );
            reaction.cooldown = EditorGUILayout.FloatField(cooldownLabel, reaction.cooldown);
        }

        DrawPropertiesExcluding(serializedObject, ExcludedProperties);

        serializedObject.ApplyModifiedProperties();
        EditorApplication.update.Invoke();
    }
}