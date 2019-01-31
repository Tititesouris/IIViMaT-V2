using Interaction.Actors;
using UnityEditor;

[CustomEditor(typeof(SpectatorActor), true)]
public class SpectatorActorEditor : ActorEditor
{

    protected override void DrawGui()
    {
        base.DrawGui();
        EditorGUIUtility.labelWidth = 180;
    }
}