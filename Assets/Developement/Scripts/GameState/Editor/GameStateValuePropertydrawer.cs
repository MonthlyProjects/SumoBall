using UnityEditor;
using UnityEngine;
[CustomPropertyDrawer(typeof(StateValues))]
public class GameStateValuePropertydrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);


        Color backGround = GUI.backgroundColor;
        Color content = GUI.contentColor;
        Color lightRed = new Color(1f, 0.25f, 0.25f, 1f); // R=1, G=0.5, B=0.5, A=0.5
        Color lightGreen = new Color(0.25f, 1f, 0.25f, 1f); // R=0.5, G=1, B=0.5, A=0.5

        // Iterate over the properties of StateValues
        SerializedProperty iterator = property.Copy();

        iterator.Next(true);

        do
        {
            // Change color based on the value of Override
            SerializedProperty overrideProp = iterator.FindPropertyRelative("Override");

            if (overrideProp != null && overrideProp.boolValue)
            {
                GUI.backgroundColor = lightGreen;
                GUI.contentColor = lightGreen;
            }
            else
            {
                GUI.backgroundColor = lightRed;
                GUI.contentColor = lightRed;
            }

            EditorGUI.PropertyField(position, iterator, true);
            position.y += EditorGUI.GetPropertyHeight(iterator, GUIContent.none, true);
        }
        while (iterator.NextVisible(false));

        GUI.backgroundColor = backGround;
        GUI.contentColor = content;

        EditorGUI.indentLevel--;
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float height = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        SerializedProperty iterator = property.Copy();
        iterator.Next(true);
        do
        {
            height += EditorGUI.GetPropertyHeight(iterator, GUIContent.none, true);
        }
        while (iterator.NextVisible(false));
        return height;
    }
}
