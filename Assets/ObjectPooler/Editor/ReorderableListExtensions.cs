using System;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using Object = System.Object;

public static class ReorderableListExtensions
{
    
    public static ReorderableList SimpleReorderableList(SerializedObject serializedObject, string propertyName, string label)
    {
        ReorderableList list = new ReorderableList(serializedObject, serializedObject.FindProperty(propertyName), true, true,
            true, true);
        
        list.drawFooterCallback = (Rect rect) => { };

        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            if (RemoveElementButton(rect))
            {
                list.serializedProperty.GetArrayElementAtIndex(index).objectReferenceValue = null;
                list.serializedProperty.DeleteArrayElementAtIndex(index);
                list.serializedProperty.serializedObject.ApplyModifiedProperties();
            }
            else
            {
                if (IndexWasOutOfBounds(index, list))
                    return;
                var element = list.serializedProperty.GetArrayElementAtIndex(index);
                Rect elementRect = new Rect(rect.x, rect.y, 2 * rect.width / 3, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(elementRect,element, GUIContent.none);
            }
        };

        return list;
    }

    public static bool RemoveElementButton(Rect rect)
    {
        if (GUI.Button(
            new Rect(rect.x + 2 * rect.width / 3, rect.y, rect.width / 3, EditorGUIUtility.singleLineHeight),
            "Remove"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool IndexWasOutOfBounds(int elementIndex, ReorderableList list)
    {
        if (elementIndex > list.serializedProperty.arraySize - 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void HandleShowStatus(ref bool showValue, ref ReorderableList list, string showLabel, string hideLabel)
    {
        if (showValue)
        {
            if (GUILayout.Button(hideLabel))
                showValue = false;
        }
        else
        {
            if (GUILayout.Button(showLabel))
                showValue = true;
        }
        
        if(showValue)
            list.DoLayoutList();
    }

    public static void HandleDragAndDrop(Rect addElementArea, Action tryAddToList)
    {
        Event currentEvent = Event.current;
        switch (currentEvent.type)
        {
            case EventType.DragUpdated:
            case EventType.DragPerform:
                if (!addElementArea.Contains(currentEvent.mousePosition))
                    return;
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                if (currentEvent.type == EventType.DragPerform)
                {
                    DragAndDrop.AcceptDrag();
                    tryAddToList();
                }

                break;
        }
    }
}
