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
        SerializedProperty preconditions_list_aux;

        public override void OnInspectorGUI()
        {
            //DrawDefaultInspector(); //Default selector
            DisplayDefaultProperties(); //Default selector by custom code
            //DisplayActualProperties(); //Other properties like buttons, ...

            #region TEST
            //serializedObject.FindProperty("resourceStruct").hide
            //EditorGUILayout.PropertyField(serializedObject.FindProperty("resourceStruct"));
            #endregion
            
        }
        void AddPrecondition(SerializedProperty list)
        {
            list.arraySize++; //Duplicates the last element
        }

        public static void Show(SerializedProperty list)
        {
            int selectedValue;

            EditorGUILayout.PropertyField(list, false);
            EditorGUI.indentLevel += 1;
            if (list.isExpanded)
            {
                EditorGUILayout.PropertyField(list.FindPropertyRelative("Array.size")); //Print "Size" field in inspector
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
                                break;
                            case 1: //PositionResource
                                EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i).FindPropertyRelative("p_resource"));
                                break;
                            case 2: //InventoryResource
                                EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i).FindPropertyRelative("i_resource"));
                                break;
                            case 3: //StatusResource
                                EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i).FindPropertyRelative("s_resource"));
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
            serializedObject.Update();
            //Default properties
            EditorGUILayout.PropertyField(serializedObject.FindProperty("actionName"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("actionAnimation"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("target"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("inRange"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("duration"));
            //Lists:
            ADAPT_UI_Actions.Show(serializedObject.FindProperty("preconditions_list"));
            ADAPT_UI_Actions.Show(serializedObject.FindProperty("effects_list"));
            serializedObject.ApplyModifiedProperties();
            /*EditorGUILayout.PropertyField(serializedObject.FindProperty("preconditions_list"));
            SerializedProperty arrayProp = serializedObject.FindProperty("preconditions_list");
            for (int i = 0; i < arrayProp.arraySize; i++)
            {
                // This will display an Inspector Field for each array item (layout this as desired)
                SerializedProperty value = arrayProp.GetArrayElementAtIndex(i);
                EditorGUILayout.PropertyField(value);
            }*/
            EditorGUILayout.PropertyField(serializedObject.FindProperty("totalPriority"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("running"));
        }

        void DisplayActualProperties() //Allows to display Action's properties
        {
            preconditions_list_aux = serializedObject.FindProperty("preconditions_list");
            #region Preconditions_Button
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button(add_precondition_button_text, GUILayout.Width(500)))
            {
                AddPrecondition(preconditions_list_aux);
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            serializedObject.ApplyModifiedProperties();
            #endregion
        }

    }
}
