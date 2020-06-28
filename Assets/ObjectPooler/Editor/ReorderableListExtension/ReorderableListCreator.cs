using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using Object = System.Object;

public static class ReorderableListCreator
{
    public static ReorderableList SimpleListWithRemoveButtonOnEachElement(SerializedObject serializedObject, SerializedProperty listProperty, bool showDefaultButtons)
    {
        ReorderableList list = new ReorderableList(serializedObject, listProperty, true, true,
            true, true);

        if (!showDefaultButtons)
        {
            list.drawFooterCallback = (Rect rect) => { };
        }

        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            Rect elementRect = new Rect(rect.x, rect.y, 2 * rect.width / 3, EditorGUIUtility.singleLineHeight);
            Rect removeButtonRect = new Rect(rect.x + 2 * rect.width/3, rect.y, rect.width/3, EditorGUIUtility.singleLineHeight);

            if (ReorderableListTools.RemoveButton(removeButtonRect))
            {
                list.serializedProperty.GetArrayElementAtIndex(index).objectReferenceValue = null;
                list.serializedProperty.DeleteArrayElementAtIndex(index);
                list.serializedProperty.serializedObject.ApplyModifiedProperties();
            }
            else
            {
                if (ReorderableListTools.IndexWasOutOfBounds(list, index))
                    return;
                var element = list.serializedProperty.GetArrayElementAtIndex(index);
                EditorGUI.PropertyField(elementRect,element, GUIContent.none);
            }
        };

        return list;
    }
    
    public static ReorderableList SimpleList(SerializedObject serializedObject, SerializedProperty listProperty)
    {
        ReorderableList list = new ReorderableList(serializedObject, listProperty, true, true,
            true, true);

        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            var element = list.serializedProperty.GetArrayElementAtIndex(index);
            EditorGUI.PropertyField(rect, element, GUIContent.none);
        };

        return list;
    }
}
