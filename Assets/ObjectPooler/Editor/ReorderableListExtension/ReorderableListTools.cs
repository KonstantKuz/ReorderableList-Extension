using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using Object = System.Object;

public static class ReorderableListTools
{
    public static void HeaderLabel(ReorderableList list, string label)
    {
        list.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, label);
        };
    }
    
    public static Rect DragnDropAreaOnHeader(ReorderableList list, string listPropertyName)
    {
        Rect dragnDropArea = Rect.zero;
        list.drawHeaderCallback = delegate(Rect rect)
        {
            dragnDropArea = rect;
        };
        return dragnDropArea;
    }
    
    public static bool RemoveButton(Rect rect)
    {
        if (GUI.Button(rect, "Remove"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool IndexWasOutOfBounds(ReorderableList list, int elementIndex)
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
    
    private static Dictionary<ReorderableList, bool> ShowStatuses = new Dictionary<ReorderableList, bool>();
    public static void HandleShowStatusByButton(ref ReorderableList list, string showLabel, string hideLabel)
    {
        if (ShowStatuses.ContainsKey(list))
        {
            if (ShowStatuses[list])
            {
                if (GUILayout.Button(hideLabel))
                    ShowStatuses[list] = false;
            }
            else
            {
                if (GUILayout.Button(showLabel))
                    ShowStatuses[list] = true;
            }
        }
        else
        {
            ShowStatuses.Add(list, new bool());
        }
        
        if(ShowStatuses[list])
            list.DoLayoutList();
    }

    public static void AddElementsByDragAndDropWithType<T>(SerializedProperty listProperty, Rect dragnDropArea)
    {
        Event currentEvent = Event.current;
        switch (currentEvent.type)
        {
            case EventType.DragUpdated:
            case EventType.DragPerform:
                if (!dragnDropArea.Contains(currentEvent.mousePosition))
                    return;
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                if (currentEvent.type == EventType.DragPerform)
                {
                    DragAndDrop.AcceptDrag();
                    TryAddElementsFromDragAndDrop<T>(listProperty);
                }

                break;
        }
    }

    private static void TryAddElementsFromDragAndDrop<T>(SerializedProperty listProperty) //where T : ScriptableObject
    {
        foreach (Object draggedObject in DragAndDrop.objectReferences)
        {
            T typeToTryAdd = (T) draggedObject;
            if(typeToTryAdd == null)
                return;
            
            UnityEngine.Object objectToAdd = (UnityEngine.Object)(typeToTryAdd as Object);

            if (ElementContainsInArray(listProperty, objectToAdd))
            {
                Debug.LogWarning($"Element {objectToAdd.name} is already exists in {listProperty.name} list!");
            }
            else
            {
                listProperty.arraySize++;
                listProperty.GetArrayElementAtIndex(listProperty.arraySize - 1).objectReferenceValue = null;
                listProperty.GetArrayElementAtIndex(listProperty.arraySize - 1).objectReferenceValue = objectToAdd;
            }
        }
    }

    private static bool ElementContainsInArray(SerializedProperty listProperty, Object obj)
    {
        bool contains = false;
        
        for (int i = 0; i < listProperty.arraySize; i++)
        {
            if (listProperty.GetArrayElementAtIndex(i).objectReferenceValue == obj)
            {
                contains = true;
                break;
            }
        }

        return contains;
    }
}
