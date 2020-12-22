using System;
using UnityEditor;
// Change 'Type' to base class name
// in which array is needs to be drawn as reorderable
[CustomEditor(typeof(Type))]
public class ReorderableDrawerTemplate : Editor
{
    // List/Array property name in base class
    private string arrayPropertyName = "yourArrayOrListPropertyName";
    
    // Nested properties names for example like in ExamplePropertiesHolder
    private string arrayElementPropertiesNames = { "propertyName1", "propertyName2" };
    
    private ReorderableDrawer arrayDrawer;
    private void OnEnable()
    {
        arrayDrawer = new ReorderableDrawer(ReorderableType.WithRemoveButtons, false);
        // or use
        arrayDrawer = new ReorderableDrawer(ReorderableType.WithRemoveButtons, arrayElementPropertiesNames);
        // to draw ExamplePropertiesHolder[] (or List) with its nested properties on one line
        
        arrayDrawer.SetUp(serializedObject, arrayPropertyName);
    }
    
    public override void OnInspectorGUI()
    {
        // If you have some different arrays in one class use
        // DrawPropertiesExcluding(serializedObject,  new string [] { arrayPropertyName1, arrayPropertyName2, ...} );
        serializedObject.Update();
        DrawPropertiesExcluding(serializedObject,  arrayPropertyName);
        serializedObject.ApplyModifiedProperties();

        arrayDrawer.Draw(serializedObject, target);
    }
}

[System.Serializable]
public class ExamplePropertiesHolder
{
    public float property1;
    public int property2;
}