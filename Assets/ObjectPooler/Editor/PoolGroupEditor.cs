﻿using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(PoolGroup))]
public class PoolGroupEditor : Editor
{
    private string arrayPropertyName = "poolsInGroup";
    
    private ReorderableDrawer<Pool> poolsDrawer;
    private void OnEnable()
    {
        poolsDrawer = new ReorderableDrawer<Pool>(ReorderableType.WithRemoveButtons, false);
        poolsDrawer.SetUp(serializedObject, arrayPropertyName);
    }

    public override void OnInspectorGUI()
    {
        DrawPropertiesExcluding(serializedObject,  arrayPropertyName);
        poolsDrawer.Draw(serializedObject, target);
    }
}

// [CustomEditor(typeof(PoolGroup))]
// public class PoolGroupEditor : Editor
// {
//     private SerializedProperty poolsInGroupProperty;
//     private ReorderableList poolsInGroupList;
//     private Rect addPoolsToGroupDragnDropArea;
//
//     private const string poolsInGroupPropertyName = "poolsInGroup";
//     private string[] nonDrawingProperties = new string[] { poolsInGroupPropertyName };
//
//     private void OnEnable()
//     {
//         poolsInGroupProperty = serializedObject.FindProperty(poolsInGroupPropertyName);
//         
//         DrawPoolsInGroupList();
//     }
//
//     private void DrawPoolsInGroupList()
//     {
//         poolsInGroupList = ReorderableListCreator.SimpleListWithRemoveButtonOnEachElement(serializedObject, poolsInGroupProperty, false);
//         addPoolsToGroupDragnDropArea = ReorderableListTools.DragnDropAreaOnHeader(poolsInGroupList);
//     }
//
//     public override void OnInspectorGUI()
//     {
//         serializedObject.Update();
//
//         DrawPropertiesExcluding(serializedObject, nonDrawingProperties);
//
//         ReorderableListTools.HandleShowStatusByButton(poolsInGroupList);
//         
//         ReorderableListTools.AddElementsByDragAndDropWithType<Pool>(poolsInGroupProperty, addPoolsToGroupDragnDropArea);
//
//         if (GUI.changed)
//         {
//             PoolGroup poolGroup = (PoolGroup)target;
//             Undo.RecordObject(poolGroup, $"Pool group Modify");
//             EditorUtility.SetDirty(poolGroup);
//         }
//
//         serializedObject.ApplyModifiedProperties();
//     }
// }