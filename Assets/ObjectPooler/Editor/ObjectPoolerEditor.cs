using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using Object = System.Object;

[CustomEditor(typeof(ObjectPooler))]
public class ObjectPoolerEditor : Editor
{
    private SerializedProperty poolsProperty;
    private ReorderableList poolsList;
    private Rect addPoolsArea;

    private SerializedProperty poolGroupsProperty;
    private ReorderableList groupsList;
    private Rect addGroupsArea;

    private const string poolsPropertyName = "pools";
    private const string poolGroupsPropertyName = "poolGroups";

    private const string poolsHeaderLabel = "Pools (Drag&Drop elements here)";
    private const string poolGroupsHeaderLabel = "Pool groups (Drag&Drop elements here)";
    
    private void OnEnable()
    {
        poolsProperty = serializedObject.FindProperty(poolsPropertyName);
        poolGroupsProperty = serializedObject.FindProperty(poolGroupsPropertyName);
        
        DrawPools();
        DrawPoolGroups();
    }

    private void DrawPools()
    {
        poolsList = ReorderableListCreator.SimpleListWithRemoveButtonOnEachElement(serializedObject, poolsProperty, false);
        poolsList.drawHeaderCallback = (Rect rect) =>
        {
            addPoolsArea = rect;
            EditorGUI.LabelField(rect, poolsHeaderLabel);
        };
    }

    private void DrawPoolGroups()
    {
        groupsList = ReorderableListCreator.SimpleListWithRemoveButtonOnEachElement(serializedObject, poolGroupsProperty, false);
        groupsList.drawHeaderCallback = (Rect rect) =>
        {
            addGroupsArea = rect;
            EditorGUI.LabelField(rect, poolGroupsHeaderLabel);
        };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        ReorderableListTools.HandleShowStatusByButton(ref poolsList, "Show pools", "Hide pools");
        ReorderableListTools.HandleShowStatusByButton(ref groupsList, "Show groups", "Hide groups");

        ReorderableListTools.AddElementsByDragAndDropWithType<Pool>(poolsProperty, addPoolsArea);
        ReorderableListTools.AddElementsByDragAndDropWithType<PoolGroup>(poolGroupsProperty, addGroupsArea);
        
        serializedObject.ApplyModifiedProperties();
    }
}
