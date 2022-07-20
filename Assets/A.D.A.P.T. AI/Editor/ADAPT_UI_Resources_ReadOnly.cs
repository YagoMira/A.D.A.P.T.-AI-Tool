using UnityEditor;
using UnityEngine;

//Display texts as "disabled" in the inspector. 
//For call, use: [ReadOnly]
[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ADAPT_UI_Resources_ReadOnly : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property,
                                            GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public override void OnGUI(Rect position,
                               SerializedProperty property,
                               GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
}
