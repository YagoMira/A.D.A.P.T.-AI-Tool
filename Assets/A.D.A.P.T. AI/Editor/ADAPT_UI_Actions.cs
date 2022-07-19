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
            DrawDefaultInspector();
            DisplayActualProperties();
        }
        void AddPrecondition(SerializedProperty list)
        {
            list.arraySize++; //Duplicates the last element
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
