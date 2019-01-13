using System.Collections.Generic;
using System.Linq;
using Interaction.Actors;
using Interaction.Reactions.Appearance;
using Interaction.Reactions.Transform;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CameraActor), true)]
public class CameraActorEditor : IivimatEditor
{
    private SerializedProperty _triggerProximityActions;

    private SerializedProperty _maxProximityRange;

    private SerializedProperty _triggerGazeActions;

    private SerializedProperty _triggerGaze360Actions;

    private SerializedProperty _maxGazeRange;

    private SerializedProperty _goThroughObjects;

    private SerializedProperty _nbObjectsToTrigger;

    protected override void LoadGui()
    {
        _triggerProximityActions = serializedObject.FindProperty("triggerProximityActions");
        _maxProximityRange = serializedObject.FindProperty("maxProximityRange");
        _triggerGazeActions = serializedObject.FindProperty("triggerGazeActions");
        _triggerGaze360Actions = serializedObject.FindProperty("triggerGaze360Actions");
        _maxGazeRange = serializedObject.FindProperty("maxGazeRange");
        _goThroughObjects = serializedObject.FindProperty("goThroughObjects");
        _nbObjectsToTrigger = serializedObject.FindProperty("nbObjectsToTrigger");
    }

    protected override void DrawGui()
    {
        var actor = (CameraActor) target;
        EditorGUIUtility.labelWidth = 180;
        
        EditorGUILayout.PropertyField(_triggerProximityActions);
        if (actor.triggerProximityActions)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(_maxProximityRange);
            EditorGUI.indentLevel--;
        }

        EditorGUILayout.PropertyField(_triggerGazeActions);
        GUI.enabled = false;
        EditorGUILayout.PropertyField(_triggerGaze360Actions);
        GUI.enabled = true;
        if (actor.triggerGazeActions)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(_maxGazeRange);
            EditorGUILayout.PropertyField(_goThroughObjects);
            EditorGUILayout.PropertyField(_nbObjectsToTrigger);
            EditorGUI.indentLevel--;
        }
    }

    protected override IEnumerable<string> GetIgnoredFields()
    {
        return new[]
        {
            "triggerProximityActions", "maxProximityRange", "triggerGazeActions", "triggerGaze360Actions",
            "maxGazeRange", "goThroughObjects", "nbObjectsToTrigger"
        };
    }
}