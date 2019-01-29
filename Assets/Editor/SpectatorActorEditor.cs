using System.Collections.Generic;
using System.Linq;
using Interaction.Actors;
using Interaction.Reactions.Appearance;
using Interaction.Reactions.Transform;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpectatorActor), true)]
public class SpectatorActorEditor : IivimatEditor
{
    private SerializedProperty _interactionReach;

    private SerializedProperty _gazeThroughObjects;

    protected override void LoadGui()
    {
        _interactionReach = serializedObject.FindProperty("interactionReach");
        _gazeThroughObjects = serializedObject.FindProperty("gazeThroughObjects");
    }

    protected override void DrawGui()
    {
        var actor = (SpectatorActor) target;
        EditorGUIUtility.labelWidth = 180;

        EditorGUILayout.PropertyField(_interactionReach);
        EditorGUILayout.PropertyField(_gazeThroughObjects);
    }

    protected override IEnumerable<string> GetIgnoredFields()
    {
        return new[]
        {
            "interactionReach", "gazeThroughObjects"
        };
    }
}