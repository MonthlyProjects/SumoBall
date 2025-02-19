using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameStateDeboger))]
public class GameStateDebogerEditor : Editor
{
    private bool modifyValues = false;
    private bool showButtonAdders = false;
    public override void OnInspectorGUI()
    {
        if(Application.isPlaying)
        {
            // Cast the target object to GameState
            GameStateDeboger gameStateDeboger = (GameStateDeboger)target;

            // Iterate over each StateValue in StateValues
            foreach (var stateValue in gameStateDeboger.StateValues.GetType().GetFields())
            {
                // Get the StateValueData object
                StateValueDataBase stateValueData = (StateValueDataBase)stateValue.GetValue(gameStateDeboger.StateValues);

                // Check if the OverrideValue is true
                if (stateValueData.Override)
                {
                    // Draw the Value field based on its type
                    DrawValueField(stateValueData, stateValue.Name);
                }
            }

            // Draw Active States
            GUILayout.Space(10);

            GUILayout.Label("All Current Active States In Priority Order:");
            for(int i = 0; i < gameStateDeboger.ActiveStates.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label(i + ": " + gameStateDeboger.ActiveStates[i].name, GUILayout.Width(EditorGUIUtility.labelWidth));
                EditorGUILayout.EndHorizontal();
            }

            //Add buttons to add or remove states

            if(Application.isPlaying && GameStateManager.Instance != null)
            {
                //Setup
                List<GameState> AllGameStates = GameStateManager.Instance.AllGameStates.gameStatesSortedByPriority;
                List<GameState> inactive = new();
                List<GameState> active = new();
                foreach(GameState state in AllGameStates)
                {
                    if(state.IsActive)
                    {
                        active.Add(state);
                    }
                    else
                    {
                        inactive.Add(state);
                    }
                }



                Color defaultColor = GUI.backgroundColor;
                if(!showButtonAdders)
                {
                    GUI.backgroundColor = Color.green;
                    if (GUILayout.Button("Add/Remove States"))
                    {
                        showButtonAdders = true;
                    }
                }
                else
                {
                    GUI.backgroundColor = Color.red;
                    if (GUILayout.Button("Hide Add/Remove States"))
                    {
                        showButtonAdders = false;
                    }

                    GUILayout.Space(10);

                    GUI.backgroundColor = Color.blue;
                    foreach (GameState state in inactive)
                    {
                        if (GUILayout.Button("Add " + state.name))
                        {
                            GameStateManager.Instance.AddState(state);
                        }
                    }

                    GUILayout.Space(10);

                    GUI.backgroundColor = Color.yellow;
                    foreach (GameState state in active)
                    {
                        if (GUILayout.Button("Remove " + state.name))
                        {
                            GameStateManager.Instance.RemoveState(state);
                        }
                    }
                }
            }


        }
        else
        {
            GUILayout.Label("THE APPLICATION IS NOT PLAYING");
        }
    }

    private void DrawValueField(StateValueDataBase stateValueData, string fieldName)
    {
        // Get the type of the value
        System.Type valueType = stateValueData.GetType().BaseType.GetGenericArguments()[0];

        // Draw the field name followed by the appropriate field based on the value type
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label(fieldName, GUILayout.Width(EditorGUIUtility.labelWidth));
        // Draw the appropriate field based on the value type
        if (valueType == typeof(float))
        {
            EditorGUILayout.LabelField(((StateValueData<float>)stateValueData).Value.ToString());
        }
        else if (valueType == typeof(bool))
        {
            EditorGUILayout.LabelField(((StateValueData<bool>)stateValueData).Value.ToString());
        }
        // Add more type checks for other types as needed

        EditorGUILayout.EndHorizontal();
    }
}
