using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using Object = System.Object;

[CustomEditor(typeof(ObjectPooler))]
public class ObjectPoolerEditor : Editor
{
    private ReorderableList poolsList;
    private Rect addPoolsArea;
    private bool showPools;
    
    private ReorderableList groupsList;
    private Rect addGroupsArea;
    private bool showGroups;
    
    private void OnEnable()
    {
        DrawPools();
        DrawPoolGroups();
    }

    private void DrawPools()
    {
        poolsList = ReorderableListExtensions.SimpleReorderableList(serializedObject, "pools", "Pools");
        poolsList.drawHeaderCallback = (Rect rect) =>
        {
            addPoolsArea = rect;
            EditorGUI.LabelField(rect, "Pools");
        };
    }

    private void DrawPoolGroups()
    {
        groupsList = ReorderableListExtensions.SimpleReorderableList(serializedObject, "poolGroups", "Pool Groups");
        groupsList.drawHeaderCallback = (Rect rect) =>
        {
            addGroupsArea = rect;
            EditorGUI.LabelField(rect, "Pool groups");
        };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        ReorderableListExtensions.HandleShowStatus(ref showPools, ref poolsList, "Show pools", "Hide pools");
        ReorderableListExtensions.HandleShowStatus(ref showGroups, ref groupsList, "Show groups", "Hide groups");

        ReorderableListExtensions.HandleDragAndDrop(addPoolsArea, TryAddElementsToPools);
        ReorderableListExtensions.HandleDragAndDrop(addGroupsArea, TryAddElementsToGroups);
        
        serializedObject.ApplyModifiedProperties();
    }

    private void TryAddElementsToPools()
    {
        ObjectPooler pooler = (ObjectPooler) target;
    
        foreach (Object draggedObject in DragAndDrop.objectReferences)
        {
            Pool draggedPool = draggedObject as Pool;
            if (draggedPool == null)
                return;
            if(pooler.EditorOnlyPools == null)
                pooler.EditorOnlyPools = new List<Pool>();
            if (pooler.EditorOnlyPools.Contains(draggedPool))
                Debug.LogWarning(
                    $"Pool with prefab name {draggedPool.prefab.name} already exists in pools.");
            else
                pooler.EditorOnlyPools.Add(draggedPool);

            pooler.EditorOnlyPools = pooler.EditorOnlyPools;
        }
    }

    private void TryAddElementsToGroups()
    {
        ObjectPooler pooler = (ObjectPooler) target;
        foreach (Object draggedObject in DragAndDrop.objectReferences)
        {
            PoolGroup draggedGroup = draggedObject as PoolGroup;
            if (draggedGroup == null)
                return;
            if(pooler.EditorOnlyPoolGroups == null)
                pooler.EditorOnlyPoolGroups = new List<PoolGroup>();
            if (pooler.EditorOnlyPoolGroups.Contains(draggedGroup))
                Debug.LogWarning($"Pool group with tag {draggedGroup.groupTag} already exists in pool groups.");
            else
                pooler.EditorOnlyPoolGroups.Add(draggedGroup);
            
            pooler.EditorOnlyPoolGroups = pooler.EditorOnlyPoolGroups;
        }
    }
}
