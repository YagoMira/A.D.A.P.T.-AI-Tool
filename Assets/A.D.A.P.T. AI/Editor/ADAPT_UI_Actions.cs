using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ADAPT.UI
{
    //Boolean set to true allows CustomEditor for inheritance classes.
    [CustomEditor(typeof(Action), true), CanEditMultipleObjects]
    public class ADAPT_UI_Actions : Editor //A.D.A.P.T Use "Editor" custom editor for actually exists script, and custom window for actions relationated with the tool management.
    {
        string add_precondition_button_text = "Add Precondition";
        string add_effect_button_text = "Add Effect";
        static string preconditions_list_string = "preconditions_list";
        SerializedProperty preconditions_list_aux, effects_list_aux;
     

        public override void OnInspectorGUI()
        {
            //DrawDefaultInspector(); //Default selector
            DisplayDefaultProperties(); //Default selector by custom code
            DisplayOtherProperties(); //Other properties like buttons, ...
            
        }

        static void SetElementsToNull(string list, Action a, int index, int flag)
        {
            //Flag -> 0 : WorldResource / 1 : PositionResource / 2 : InventoryResource / 3 : StatusResource
            //When receive a flag, set null all other elements of the preconditions/effects list in specific index!.

            if(flag == 0)
            {
                if(list.Equals(preconditions_list_string)) //Preconditions_List
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
            else if(flag == 1)
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

        void AddNewElement(string list)
        {  
            Action a = target as Action;

            if (list.Equals(preconditions_list_string)) //Preconditions list
                a.preconditions_list.Add(new Action.ResourceStruct("", new WorldResource()));
            else //Effects list
                a.effects_list.Add(new Action.ResourceStruct("", new WorldResource()));

            serializedObject.ApplyModifiedProperties();
        }

        public static void Show(SerializedProperty list, Action a)
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
                    if(list.GetArrayElementAtIndex(i).isExpanded)
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
                                SetElementsToNull(list.name, a, i, 0);
                                break;
                            case 1: //PositionResource
                                EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i).FindPropertyRelative("p_resource"));
                                SetElementsToNull(list.name, a, i, 1);
                                break;
                            case 2: //InventoryResource
                                EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i).FindPropertyRelative("i_resource"));
                                SetElementsToNull(list.name, a, i, 2);
                                break;
                            case 3: //StatusResource
                                EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i).FindPropertyRelative("s_resource"));
                                SetElementsToNull(list.name, a, i, 3);
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
            Action a = target as Action;
            serializedObject.Update();
            //Default properties
            EditorGUILayout.PropertyField(serializedObject.FindProperty("actionName"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("running"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("finished"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("actionAnimation"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("hasTarget")); //Bool to check if the actual Action will have some target.
            if(a.hasTarget) //In case of have some target...
            {
                EditorGUI.indentLevel += 1;
                EditorGUILayout.PropertyField(serializedObject.FindProperty("target"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("stopDistance"));
                EditorGUI.indentLevel -= 1;
            }
            EditorGUILayout.PropertyField(serializedObject.FindProperty("inRange"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("duration"));
            //Lists:
            ADAPT_UI_Actions.Show(serializedObject.FindProperty(preconditions_list_string), a);
            ADAPT_UI_Actions.Show(serializedObject.FindProperty("effects_list"), a);
            //More Default properties:
            EditorGUILayout.PropertyField(serializedObject.FindProperty("totalPriority"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("totalCost"));
            serializedObject.ApplyModifiedProperties();
        }

        void DisplayOtherProperties()
        {
            preconditions_list_aux = serializedObject.FindProperty(preconditions_list_string);
            effects_list_aux = serializedObject.FindProperty("effects_list");

            #region Buttons
            GUI.backgroundColor = Color.cyan; //Color for buttons

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button(add_precondition_button_text, GUILayout.Width(250)))
            {
                AddNewElement(preconditions_list_aux.name);
            }
            if (GUILayout.Button(add_effect_button_text, GUILayout.Width(250)))
            {
                AddNewElement(effects_list_aux.name);
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            serializedObject.ApplyModifiedProperties();
            #endregion
        }

    }
}
