using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using Object = System.Object;

[CustomEditor(typeof(ObjectPooler))]
public class ObjectPoolerEditor : Editor
{
    private ReorderableDrawer<Pool> poolsDrawer;
    private ReorderableDrawer<PoolGroup> groupsDrawer;
    
    private const string poolsPropertyName = "pools";
    private const string poolGroupsPropertyName = "poolGroups";

    private void OnEnable()
    {
        poolsDrawer = new ReorderableDrawer<Pool>(ReorderableType.WithRemoveButtons, false);
        groupsDrawer = new ReorderableDrawer<PoolGroup>(ReorderableType.WithRemoveButtons, false);
        
        poolsDrawer.SetUp(serializedObject, poolsPropertyName);
        groupsDrawer.SetUp(serializedObject, poolGroupsPropertyName);
    }

    public override void OnInspectorGUI()
    {
        DrawPropertiesExcluding(serializedObject, new string[] {poolsPropertyName, poolGroupsPropertyName});
        
        poolsDrawer.Draw(serializedObject, target );
        groupsDrawer.Draw(serializedObject, target);
    }
}
