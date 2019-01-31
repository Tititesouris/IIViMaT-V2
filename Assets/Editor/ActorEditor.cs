using System.Collections.Generic;
using Interaction.Actors;
using UnityEditor;

[CustomEditor(typeof(Actor), true)]
public class ActorEditor : IivimatEditor
{
    protected override void LoadGui()
    {
        
    }

    protected override void DrawGui()
    {
        EditorGUILayout.Space();
    }

    protected override IEnumerable<string> GetIgnoredFields()
    {
        return new List<string>();
    }
}