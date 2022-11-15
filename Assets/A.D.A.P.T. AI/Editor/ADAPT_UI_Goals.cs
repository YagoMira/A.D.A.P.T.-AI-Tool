using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;


[CustomPropertyDrawer(typeof(Agent.Goal))]
public class ADAPT_UI_Goals : PropertyDrawer
{
    SerializedProperty hasAction, selectedType, precondition, w_resource, p_resource, i_resource, s_resource;
    //SerializedProperty resourceName, resourceEnumType, priority, limit, resource_value;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        hasAction = property.FindPropertyRelative("hasAction"); 
        precondition = property.FindPropertyRelative("goal_precondition");
        selectedType = precondition.FindPropertyRelative("selectedType");

        w_resource = precondition.FindPropertyRelative("w_resource");
        p_resource = precondition.FindPropertyRelative("p_resource");
        i_resource = precondition.FindPropertyRelative("i_resource");
        s_resource = precondition.FindPropertyRelative("s_resource");


        position.height = 16;
        property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, label);

        if (property.isExpanded)
        {
            Rect contentPosition = EditorGUI.PrefixLabel(position, new GUIContent(""));

            contentPosition.x = 33;
            contentPosition.y += 18;
            contentPosition.width = position.width - 18;
            contentPosition.height = position.height + 3;
            EditorGUIUtility.labelWidth = 165.0f;

            EditorGUI.PropertyField(contentPosition, precondition.FindPropertyRelative("key"), new GUIContent("GoalName"));
            contentPosition.y += 21;
            EditorGUI.PropertyField(contentPosition, selectedType, new GUIContent("Type"));
            contentPosition.y += 21;
            EditorGUI.PropertyField(contentPosition, precondition, new GUIContent("Goal Precondition"));
            if (precondition.isExpanded)
            {
                contentPosition.y += 21;
                switch (selectedType.enumValueIndex)
                {
                    case 0: //WorldResource
                        EditorGUI.PropertyField(contentPosition, w_resource.FindPropertyRelative("resourceName"), new GUIContent("resourceName"));
                        w_resource.FindPropertyRelative("resourceName").stringValue = precondition.FindPropertyRelative("key").stringValue;
                        contentPosition.y += 21;
                        EditorGUI.PropertyField(contentPosition, w_resource.FindPropertyRelative("resourceEnumType"), new GUIContent("resourceEnumType"));
                        contentPosition.y += 21;
                        EditorGUI.PropertyField(contentPosition, w_resource.FindPropertyRelative("priority"), new GUIContent("priority"));
                        contentPosition.y += 21;
                        EditorGUI.PropertyField(contentPosition, w_resource.FindPropertyRelative("limit"), new GUIContent("limit"));
                        contentPosition.y += 21;
                        EditorGUI.PropertyField(contentPosition, w_resource.FindPropertyRelative("resource_value"), new GUIContent("resource_value"));
                        break;
                    case 1: //PositionResource
                        EditorGUI.PropertyField(contentPosition, p_resource.FindPropertyRelative("resourceName"), new GUIContent("resourceName"));
                        p_resource.FindPropertyRelative("resourceName").stringValue = precondition.FindPropertyRelative("key").stringValue;
                        contentPosition.y += 21;
                        EditorGUI.PropertyField(contentPosition, p_resource.FindPropertyRelative("resourceEnumType"), new GUIContent("resourceEnumType"));
                        contentPosition.y += 21;
                        EditorGUI.PropertyField(contentPosition, p_resource.FindPropertyRelative("priority"), new GUIContent("priority"));
                        contentPosition.y += 21;
                        EditorGUI.PropertyField(contentPosition, p_resource.FindPropertyRelative("limit"), new GUIContent("limit"));
                        contentPosition.y += 21;
                        EditorGUI.PropertyField(contentPosition, p_resource.FindPropertyRelative("resource_value"), new GUIContent("resource_value"));
                        break;
                    case 2: //InventoryResource
                        EditorGUI.PropertyField(contentPosition, i_resource.FindPropertyRelative("resourceName"), new GUIContent("resourceName"));
                        i_resource.FindPropertyRelative("resourceName").stringValue = precondition.FindPropertyRelative("key").stringValue;
                        contentPosition.y += 21;
                        EditorGUI.PropertyField(contentPosition, i_resource.FindPropertyRelative("resourceEnumType"), new GUIContent("resourceEnumType"));
                        contentPosition.y += 21;
                        EditorGUI.PropertyField(contentPosition, i_resource.FindPropertyRelative("priority"), new GUIContent("priority"));
                        contentPosition.y += 21;
                        EditorGUI.PropertyField(contentPosition, i_resource.FindPropertyRelative("limit"), new GUIContent("limit"));
                        contentPosition.y += 21;
                        EditorGUI.PropertyField(contentPosition, i_resource.FindPropertyRelative("resource_value"), new GUIContent("resource_value"));
                        contentPosition.y += 21;
                        EditorGUI.PropertyField(contentPosition, i_resource.FindPropertyRelative("isConsumable"), new GUIContent("isConsumable"));
                        break;
                    case 3: //StatusResource
                        EditorGUI.PropertyField(contentPosition, s_resource.FindPropertyRelative("resourceName"), new GUIContent("resourceName"));
                        s_resource.FindPropertyRelative("resourceName").stringValue = precondition.FindPropertyRelative("key").stringValue;
                        contentPosition.y += 21;
                        EditorGUI.PropertyField(contentPosition, s_resource.FindPropertyRelative("resourceEnumType"), new GUIContent("resourceEnumType"));
                        contentPosition.y += 21;
                        EditorGUI.PropertyField(contentPosition, s_resource.FindPropertyRelative("priority"), new GUIContent("priority"));
                        contentPosition.y += 21;
                        EditorGUI.PropertyField(contentPosition, s_resource.FindPropertyRelative("limit"), new GUIContent("limit"));
                        contentPosition.y += 21;
                        EditorGUI.PropertyField(contentPosition, s_resource.FindPropertyRelative("resource_value"), new GUIContent("resource_value"));
                        break;
                }
            }
            contentPosition.y += 21;
            EditorGUI.PropertyField(contentPosition, hasAction, new GUIContent("hasAction"));

            if (hasAction.boolValue == true) //In case of has some Action
            {
                contentPosition.x += 15;
                contentPosition.y += 21;
                EditorGUIUtility.labelWidth = 150.0f;
                contentPosition.width = position.width - 33;
                EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("goal_Action"), new GUIContent("goal_Action"));

                switch (selectedType.enumValueIndex)
                {
                    case 0:
                        contentPosition.y += 21;
                        EditorGUI.PropertyField(contentPosition, w_resource.FindPropertyRelative("cost"), new GUIContent("cost"));
                        break;
                    case 1:
                        contentPosition.y += 21;
                        EditorGUI.PropertyField(contentPosition, p_resource.FindPropertyRelative("cost"), new GUIContent("cost"));
                        break;
                    case 2:
                        contentPosition.y += 21;
                        EditorGUI.PropertyField(contentPosition, i_resource.FindPropertyRelative("cost"), new GUIContent("cost"));
                        break;
                    case 3:
                        contentPosition.y += 21;
                        EditorGUI.PropertyField(contentPosition, s_resource.FindPropertyRelative("cost"), new GUIContent("cost"));
                        break;
                }
            }
                
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (property.isExpanded && property.FindPropertyRelative("hasAction").boolValue == false && !property.FindPropertyRelative("goal_precondition").isExpanded)
        {
            return 20 * 5;
        }  
        else if (property.isExpanded && property.FindPropertyRelative("hasAction").boolValue == true && !property.FindPropertyRelative("goal_precondition").isExpanded)
        {
            return 142;
        }
        else if(property.isExpanded && property.FindPropertyRelative("goal_precondition").isExpanded && property.FindPropertyRelative("hasAction").boolValue == false)
        {
            if (property.isExpanded && property.FindPropertyRelative("goal_precondition").FindPropertyRelative("selectedType").enumValueIndex == 2)
                return 25 * 9;
            else
                return 23 * 9;
        }
        else if (property.isExpanded && property.FindPropertyRelative("hasAction").boolValue == true && property.FindPropertyRelative("goal_precondition").isExpanded)
        {
            if(property.isExpanded && property.FindPropertyRelative("goal_precondition").FindPropertyRelative("selectedType").enumValueIndex == 2)
                return 26 * 10;
            else
                return 25 * 10;
        }
        else if(!property.isExpanded)
        {
            return 16;
        }
        else
        {
            return 16;
        }   
    }


}
