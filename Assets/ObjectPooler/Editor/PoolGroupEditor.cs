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
    private string[] propertiesInBaseClass = new string[] { "poolsInGroup" };
    
    private ReorderableList poolsInGroupList;
    private Rect addPoolsToGroupArea;
    private bool showPoolsInGroup;
    
    private void OnEnable()
    {
        DrawPoolsInGroupList();
    }

    private void DrawPoolsInGroupList()
    {
        poolsInGroupList = ReorderableListExtensions.SimpleReorderableList(serializedObject, "poolsInGroup", "Pools in group");
        poolsInGroupList.drawHeaderCallback = (Rect rect) =>
        {
            addPoolsToGroupArea = rect;
            EditorGUI.LabelField(rect, "Pools");
        };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawPropertiesExcluding(serializedObject, propertiesInBaseClass);

        ReorderableListExtensions.HandleShowStatus(ref showPoolsInGroup, ref poolsInGroupList, "Show pools in group",
            "Hide pools");
        ReorderableListExtensions.HandleDragAndDrop(addPoolsToGroupArea, TryAddElementsToPoolsInGroup);

        PoolGroup poolGroup = (PoolGroup)target;
        if (GUI.changed)
        {
            Undo.RecordObject(poolGroup, $"Pool group with group tag {poolGroup.groupTag} Modify");
            EditorUtility.SetDirty(poolGroup);
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void TryAddElementsToPoolsInGroup()
    {
        PoolGroup poolGroup = (PoolGroup) target;

        foreach (Object dragged_object in DragAndDrop.objectReferences)
        {
            Pool addablePool = (Pool) dragged_object;
            if (dragged_object == null)
                return;
            if(poolGroup.poolsInGroup == null)
                poolGroup.poolsInGroup = new List<Pool>();
            if (poolGroup.poolsInGroup.Contains(addablePool))
                Debug.LogWarning($"Pool with prefab name {addablePool.prefab.name} is already exists in group.");
            else
                poolGroup.poolsInGroup.Add(addablePool);
        }
    }
}