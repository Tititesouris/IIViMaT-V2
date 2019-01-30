using System.Collections.Generic;
using Interaction.Reactions.Spectator;
using UnityEditor;

[CustomEditor(typeof(ChangeViewModeReaction), true)]
public class ChangeViewModeReactionEditor : ReactionEditor
{
    private SerializedProperty _viewMode;
    
    private SerializedProperty _videoSphere;
    
    protected override void LoadGui()
    {
        base.LoadGui();
        _viewMode = serializedObject.FindProperty("viewMode");
        _videoSphere = serializedObject.FindProperty("videoSphere");
    }

    protected override void DrawGui()
    {
        base.DrawGui();
        var reaction = (ChangeViewModeReaction) target;

        EditorGUILayout.PropertyField(_viewMode);
        if (reaction.viewMode != CameraViewMode.ViewMode.FreeView)
        {
            EditorGUILayout.PropertyField(_videoSphere);
        }
    }

    protected override IEnumerable<string> GetIgnoredFields()
    {
        var ignoredFields = new List<string> {"viewMode", "videoSphere"};
        ignoredFields.AddRange(base.GetIgnoredFields());
        return ignoredFields;
    }
}