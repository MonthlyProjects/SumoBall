using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
[CustomEditor(typeof(SO_InputButton)), CanEditMultipleObjects]
public class InputDataButtonEditor : Editor
{
    private List<string> actionNames = new List<string>();
    private int selectedIndex = 0;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // Initializing Components
        SO_InputButton component = (SO_InputButton)target;
        if (component.inputActionAsset != null)
        {
            // Clear and populate action names list
            actionNames.Clear();
            foreach (var action in component.inputActionAsset.actionMaps[0].actions)
            {
                if(action.type is InputActionType.Button)
                {
                    actionNames.Add(action.name);
                }
            }

            // Find the index of the current selection to maintain state
            selectedIndex = actionNames.IndexOf(component.InputName);
            if (selectedIndex == -1)
            {
                selectedIndex = 0;
            }

            serializedObject.Update();

            // Display the dropdown list
            selectedIndex = EditorGUILayout.Popup("Select Input", selectedIndex, actionNames.ToArray());

            // Update the selected action name
            if (actionNames.Count > 0)
            {
                component.InputName = actionNames[selectedIndex];
            }

            serializedObject.ApplyModifiedProperties();
        }
        else
        {
            EditorGUILayout.HelpBox("Assign an InputActionAsset to InputButtonScriptableObject.", MessageType.Warning);
        }
    }
}

[CustomEditor(typeof(SO_InputVector)), CanEditMultipleObjects]
public class InputDataVectorEditor : Editor
{
    private List<string> actionNames = new List<string>();
    private int selectedIndex = 0;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // Initializing Components
        SO_InputVector component = (SO_InputVector)target;
        if (component.inputActionAsset != null)
        {
            // Clear and populate action names list
            actionNames.Clear();
            foreach (var action in component.inputActionAsset.actionMaps[0].actions)
            {
                if (action.type is InputActionType.Value)
                {
                    actionNames.Add(action.name);
                }
            }

            // Find the index of the current selection to maintain state
            selectedIndex = actionNames.IndexOf(component.InputName);
            if (selectedIndex == -1)
            {
                selectedIndex = 0;
            }

            serializedObject.Update();

            // Display the dropdown list
            selectedIndex = EditorGUILayout.Popup("Select Input", selectedIndex, actionNames.ToArray());

            // Update the selected action name
            if (actionNames.Count > 0)
            {
                component.InputName = actionNames[selectedIndex];
            }

            serializedObject.ApplyModifiedProperties();
        }
        else
        {
            EditorGUILayout.HelpBox("Assign an InputActionAsset to InputButtonScriptableObject.", MessageType.Warning);
        }
    }
}
