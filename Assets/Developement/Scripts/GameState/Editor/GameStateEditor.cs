using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameState))]
public class GameStateEditor : Editor
{
    private bool modifyValues = false;
    public override void OnInspectorGUI()
    {
        // Set the button background color based on modifyValues
        Color defaultColor = GUI.backgroundColor;
        if (modifyValues)
        {
            GUI.backgroundColor = Color.red;
        }

        if (GUILayout.Button("ModifyValues"))
        {
            modifyValues = !modifyValues;
        }
        GUI.backgroundColor = defaultColor;


        if (Application.isPlaying && GameStateManager.Instance != null)
        {
            GUILayout.Space(10);
            if(((GameState)target).IsActive)
            {
                if (GUILayout.Button("DesactiveState"))
                {
                    GameStateManager.Instance.RemoveState((GameState)target);
                }
            }
            else
            {
                if (GUILayout.Button("ActiveState"))
                {
                    GameStateManager.Instance.AddState((GameState)target);
                }
            }
            GUILayout.Space(10);
        }



        if (modifyValues)
        {
            base.OnInspectorGUI();
        }
        else
        {
            Color old = GUI.contentColor;
            if (Application.isPlaying)
            {
                if (((GameState)target).IsActive)
                {
                    GUI.contentColor = Color.green;
                    EditorGUILayout.LabelField("THIS STATE IS ACTIVE");
                }
                else
                {
                    GUI.contentColor = Color.red;
                    EditorGUILayout.LabelField("THIS STATE IS NOT ACTIVE");
                }
                GUI.contentColor = old;
                GUILayout.Space(10);
            }
            
            // Cast the target object to GameState
            GameState gameState = (GameState)target;
            int count = 0;
            // Iterate over each StateValue in StateValues
            foreach (var stateValue in gameState.StateValues.GetType().GetFields())
            {
                // Get the StateValueData object
                StateValueDataBase stateValueData = (StateValueDataBase)stateValue.GetValue(gameState.StateValues);

                // Check if the OverrideValue is true
                if (stateValueData.Override)
                {
                    // Draw the Value field based on its type
                    DrawValueField(stateValueData, stateValue.Name);
                    count++;
                }
            }
            if(count == 0)
            {
                EditorGUILayout.LabelField("THIS STATE DOES NOT OVERRIDE ANY VALUES");
            }
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
