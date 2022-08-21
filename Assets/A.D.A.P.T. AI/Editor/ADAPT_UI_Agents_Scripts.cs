using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace ADAPT.UI
{
    //Boolean set to true allows CustomEditor for inheritance classes.
    [CustomEditor(typeof(Agent), true), CanEditMultipleObjects]//A.D.A.P.T Use "Editor" custom editor for actually exists script, and custom window for actions relationated with the tool management.
    public class ADAPT_UI_Agents_Scripts : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector(); //Default selector
            //DisplayDefaultProperties(); //Default selector by custom code
            //DisplayOtherProperties(); //Other properties like buttons, ...

        }

        /*
         * static void SetElementsToNull(string list, Agent a, int index, int flag)
        {
            //Flag -> 0 : WorldResource / 1 : PositionResource / 2 : InventoryResource / 3 : StatusResource
            //When receive a flag, set null all other elements of the preconditions/effects list in specific index!.

            if (flag == 0)
            {
                if (list.Equals(preconditions_list_string)) //Preconditions_List
                {
                    a.preconditions_list[index].p_resource = null;
                    a.preconditions_list[index].i_resource = null;
                    a.preconditions_list[index].s_resource = null;
                }
                else //Effects List
                {
                    a.effects_list[index].p_resource = null;
                    a.effects_list[index].i_resource = null;
                    a.effects_list[index].s_resource = null;
                }
            }
            else if (flag == 1)
            {
                if (list.Equals(preconditions_list_string)) //Preconditions_List
                {
                    a.preconditions_list[index].w_resource = null;
                    a.preconditions_list[index].i_resource = null;
                    a.preconditions_list[index].s_resource = null;
                }
                else //Effects List
                {
                    a.effects_list[index].w_resource = null;
                    a.effects_list[index].i_resource = null;
                    a.effects_list[index].s_resource = null;
                }
            }
            else if (flag == 2)
            {
                if (list.Equals(preconditions_list_string)) //Preconditions_List
                {
                    a.preconditions_list[index].w_resource = null;
                    a.preconditions_list[index].p_resource = null;
                    a.preconditions_list[index].s_resource = null;
                }
                else //Effects List
                {
                    a.effects_list[index].w_resource = null;
                    a.effects_list[index].p_resource = null;
                    a.effects_list[index].s_resource = null;
                }
            }
            else if (flag == 3)
            {
                if (list.Equals(preconditions_list_string)) //Preconditions_List
                {
                    a.preconditions_list[index].w_resource = null;
                    a.preconditions_list[index].p_resource = null;
                    a.preconditions_list[index].i_resource = null;
                }
                else //Effects List
                {
                    a.effects_list[index].w_resource = null;
                    a.effects_list[index].p_resource = null;
                    a.effects_list[index].i_resource = null;
                }
            }
        }
        */

        public static void Show(SerializedProperty list, Agent a)
        {
            int selectedValue;

            EditorGUILayout.PropertyField(list, false);
            EditorGUI.indentLevel += 1;
            if (list.isExpanded)
            {

                GUI.enabled = false; //Allows Size as "ReadOnly" property.
                EditorGUILayout.PropertyField(list.FindPropertyRelative("Array.size")); //Print "Size" field in inspector
                GUI.enabled = true; //Allows Size as "ReadOnly" property.

                for (int i = 0; i < list.arraySize; i++)
                {
                    //PRINT ALL ResourceStruct fields:
                    EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i), false);
                    if (list.GetArrayElementAtIndex(i).isExpanded)
                    {
                        EditorGUI.indentLevel += 1;
                        //Print all ResourceStruct elements:
                        EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i).FindPropertyRelative("key"));
                        EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i).FindPropertyRelative("selectedType"));
                        selectedValue = list.GetArrayElementAtIndex(i).FindPropertyRelative("selectedType").enumValueIndex;

                        switch (selectedValue)
                        {
                            case 0: //WorldResource
                                EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i).FindPropertyRelative("w_resource"));
                                //SetElementsToNull(list.name, a, i, 0);
                                break;
                            case 1: //PositionResource
                                EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i).FindPropertyRelative("p_resource"));
                                //SetElementsToNull(list.name, a, i, 1);
                                break;
                            case 2: //InventoryResource
                                EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i).FindPropertyRelative("i_resource"));
                                //SetElementsToNull(list.name, a, i, 2);
                                break;
                            case 3: //StatusResource
                                EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i).FindPropertyRelative("s_resource"));
                                //SetElementsToNull(list.name, a, i, 3);
                                break;
                        }
                        EditorGUI.indentLevel -= 1;
                    }
                }
            }
            EditorGUI.indentLevel -= 1;
        }

        void DisplayDefaultProperties()
        {
            Agent a = target as Agent;
            serializedObject.Update();
            //Default properties
            EditorGUILayout.PropertyField(serializedObject.FindProperty("agentName"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("agent_goals"));
            //ADAPT_UI_Agents_Scripts.Show(serializedObject.FindProperty("goals_list"), a);
            //More Default properties:
            EditorGUILayout.PropertyField(serializedObject.FindProperty("actions"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("currentAction"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("currentGoal"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("agent"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onIdle"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("rdmDistance"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("states"));
            serializedObject.ApplyModifiedProperties();
        }
    }
}
