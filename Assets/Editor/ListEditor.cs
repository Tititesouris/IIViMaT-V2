using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ListEditor
{
    public static void Show(SerializedProperty list, Type type, string elementName, string noElementError,
        string emptyElementWarning)
    {
        if (!list.isArray)
        {
            EditorGUILayout.HelpBox(list.name + " is not a list", MessageType.Error);
            return;
        }

        EditorGUILayout.LabelField(new GUIContent(list.displayName + " (" + list.arraySize + ")"));
        EditorGUI.indentLevel++;
        if (list.arraySize == 0)
            EditorGUILayout.HelpBox(noElementError, MessageType.Error);
        else if (AnyEmptyElement(list, type))
            EditorGUILayout.HelpBox(emptyElementWarning, MessageType.Error);

        for (var i = 0; i < list.arraySize; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i),
                new GUIContent(elementName + " " + (i + 1)));
            if (GUILayout.Button(new GUIContent("X"), GUILayout.Width(18f)))
            {
                list.GetArrayElementAtIndex(i).objectReferenceValue = null;
                list.DeleteArrayElementAtIndex(i);
            }

            EditorGUILayout.EndHorizontal();
        }

        var rect = GUILayoutUtility.GetRect (new GUIContent("+"), GUI.skin.button, GUILayout.Width(100));
        rect.center = new Vector2(EditorGUIUtility.currentViewWidth / 2, rect.center.y);
        if (GUI.Button(rect, "+", GUI.skin.button))
        {
            list.InsertArrayElementAtIndex(list.arraySize);
            list.GetArrayElementAtIndex(list.arraySize - 1).objectReferenceValue = null;
        }

        EditorGUI.indentLevel--;
    }

    private static bool AnyEmptyElement(SerializedProperty list, Type type)
    {
        for (var i = 0; i < list.arraySize; i++)
        {
            var item = list.GetArrayElementAtIndex(i);
            if (type == typeof(string) && item.stringValue.Length == 0)
                return true;
            if (type == typeof(GameObject) && item.objectReferenceValue == null)
                return true;
        }

        return false;
    }
}