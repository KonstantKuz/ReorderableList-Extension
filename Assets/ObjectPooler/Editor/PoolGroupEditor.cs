using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using Object = System.Object;

[CustomEditor(typeof(PoolGroup))]
public class PoolGroupEditor : Editor
{
    private SerializedProperty poolsInGroupProperty;
    private ReorderableList poolsInGroupList;
    private Rect addPoolsToGroupDragnDropArea;

    private const string poolsInGroupPropertyName = "poolsInGroup";
    private const string poolsInGroupHeaderLabel = "Pools (Drag&Drop elements here)";
    private string[] nonDrawingProperties = new string[] { poolsInGroupPropertyName };

    private void OnEnable()
    {
        poolsInGroupProperty = serializedObject.FindProperty(poolsInGroupPropertyName);
        
        DrawPoolsInGroupList();
    }

    private void DrawPoolsInGroupList()
    {
        poolsInGroupList = ReorderableListCreator.SimpleListWithRemoveButtonOnEachElement(serializedObject, poolsInGroupProperty, false);
        addPoolsToGroupDragnDropArea = ReorderableListTools.DragnDropAreaOnHeader(poolsInGroupList, poolsInGroupPropertyName);
        ReorderableListTools.HeaderLabel(poolsInGroupList, poolsInGroupHeaderLabel);
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawPropertiesExcluding(serializedObject, nonDrawingProperties);

        ReorderableListTools.HandleShowStatusByButton(ref poolsInGroupList,
            "Show pools in group", "Hide pools");
        
        ReorderableListTools.AddElementsByDragAndDropWithType<Pool>(poolsInGroupProperty, addPoolsToGroupDragnDropArea);

        if (GUI.changed)
        {
            PoolGroup poolGroup = (PoolGroup)target;
            Undo.RecordObject(poolGroup, $"Pool group with group tag {poolGroup.groupTag} Modify");
            EditorUtility.SetDirty(poolGroup);
        }

        serializedObject.ApplyModifiedProperties();
    }
}