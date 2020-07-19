using System;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

public enum ReorderableType
{
    Simple,
    WithRemoveButtons,
}
public class ReorderableDrawer
{
    private SerializedProperty property;
    private ReorderableList reorderableList;
    private Rect dragnDropArea;
    private ReorderableType reorderableType;
    private bool showDefaultButtons;
    
    public ReorderableDrawer(ReorderableType reorderableType, bool showDefaultButtons)
    {
        this.reorderableType = reorderableType;
        this.showDefaultButtons = showDefaultButtons;
    }

    public void SetUp(SerializedObject serializedObject, string propertyName)
    {
        property = serializedObject.FindProperty(propertyName);

        switch (reorderableType)
        {
            case ReorderableType.Simple:
                reorderableList =
                    ReorderableListCreator.SimpleList(serializedObject, property, showDefaultButtons);
                break;
            case ReorderableType.WithRemoveButtons:
                reorderableList =
                    ReorderableListCreator.SimpleListWithRemoveButtonOnEachElement(serializedObject, property, showDefaultButtons);
                break;
        }
        reorderableList.drawHeaderCallback = delegate(Rect rect)
        {
            dragnDropArea = rect;
            EditorGUI.LabelField(rect, "Drag'n'Drop objects here");
        };
    }
    
    public void Draw(SerializedObject serializedObject, UnityEngine.Object target)
    {
        serializedObject.Update();

        ReorderableListTools.HandleShowStatusByButton(reorderableList);
        
        ReorderableListTools.AddElementsByDragAndDropWithType(property, dragnDropArea);

        if (GUI.changed)
        {
            Undo.RecordObject(target, $"{reorderableList.serializedProperty.name} Modify");
            EditorUtility.SetDirty(target);
        }

        serializedObject.ApplyModifiedProperties();
    }
}