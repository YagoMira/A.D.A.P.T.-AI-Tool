using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ADAPT.UI
{
    //[CustomPropertyDrawer(typeof(Action.ResourceStruct))]
    public class ADAPT_UI_ResourceStruct_DRAWER : PropertyDrawer
    {

        public void OpenEditorWindow(string index)
        {
            EditorWindow window = EditorWindow.GetWindow(typeof(ADAPT_UI_Resources_Edit));
            window.SendEvent(EditorGUIUtility.CommandEvent(index));
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            Rect rectFoldout = new Rect(position.min.x, position.min.y, position.size.x, EditorGUIUtility.singleLineHeight);
            property.isExpanded = EditorGUI.Foldout(rectFoldout, property.isExpanded, label);
            int lines = 1;
            if (property.isExpanded)
            {
                Rect rectDuration = new Rect(position.min.x, position.min.y + lines++ * EditorGUIUtility.singleLineHeight, position.size.x, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(rectDuration, property.FindPropertyRelative("key"));
                Rect rectType = new Rect(position.min.x, position.min.y + lines++ * EditorGUIUtility.singleLineHeight, position.size.x, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(rectType, property.FindPropertyRelative("selectedType"));
                Rect rectResource = new Rect(position.min.x, position.min.y + lines++ * EditorGUIUtility.singleLineHeight, position.size.x, EditorGUIUtility.singleLineHeight);
                EditorGUI.ObjectField(rectResource, property.FindPropertyRelative("resource"));
                if (GUI.Button(new Rect((position.min.x)*12, position.min.y + lines++ * EditorGUIUtility.singleLineHeight, position.size.x/2, EditorGUIUtility.singleLineHeight), "Edit"))
                {
                    OpenEditorWindow(property.FindPropertyRelative("key").stringValue);
                }
            }
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            int totalLines = 2;

            if (property.isExpanded)
            {
                totalLines += 2;
            }

            return EditorGUIUtility.singleLineHeight * totalLines + EditorGUIUtility.standardVerticalSpacing * (totalLines - 1);
        }
        // Create property container element.
        /*var container = new VisualElement();

        // Create property fields.
        var amountField = new PropertyField(property.FindPropertyRelative("amount"));
        var unitField = new PropertyField(property.FindPropertyRelative("unit"));
        var nameField = new PropertyField(property.FindPropertyRelative("name"), "Fancy Name");

        // Add fields to the container.
        container.Add(amountField);
        container.Add(unitField);
        container.Add(nameField);

        return container;*/
    }
}

