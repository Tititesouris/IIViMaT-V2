using Interaction.Actions;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Action), true)]
public class ActionEditor : Editor
{
    private static readonly string[] ExcludedProperties = {"m_Script", "specifyReactions", "reactionNames"};

    private SerializedProperty _reactionNames;

    private void OnEnable()
    {
        _reactionNames = serializedObject.FindProperty("reactionNames");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        var action = (Action) target;

        EditorGUILayout.Space();
        var specifyReactionsLabel = new GUIContent("Specify reactions",
            "If enabled, allows you to specify which reactions this action will trigger."
        );
        action.specifyReactions = EditorGUILayout.BeginToggleGroup(specifyReactionsLabel, action.specifyReactions);

        if (action.specifyReactions)
        {
            ListEditor.Show(_reactionNames, "Reaction", "No reaction name specified", "Some reaction names are empty");
        }

        EditorGUILayout.EndToggleGroup();

        DrawPropertiesExcluding(serializedObject, ExcludedProperties);

        serializedObject.ApplyModifiedProperties();
        EditorApplication.update.Invoke();
    }
}