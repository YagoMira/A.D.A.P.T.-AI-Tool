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
        protected Texture2D adapt_logo;
        protected ADAPT_UI_Resources adapt_resources;

        public override void OnInspectorGUI()
        {
            adapt_resources = new ADAPT_UI_Resources();
            //Import Logo from path
            adapt_logo = Resources.Load<Texture2D>(adapt_resources.logo_path);
            var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter };

            GUILayout.BeginHorizontal();
            GUILayout.Label(adapt_logo, style, GUILayout.MinHeight(40), GUILayout.MaxHeight(180), GUILayout.MinWidth(120), GUILayout.MaxWidth(700), GUILayout.ExpandWidth(true));
            GUILayout.EndHorizontal();
            GUILayout.Space(10);

            GUILayout.BeginVertical();
            style.normal.textColor = Color.blue;
            style.fontSize = 20;
            GUILayout.Label("AGENT", style);
            Rect rect = EditorGUILayout.GetControlRect(false, 1);
            EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));
            GUILayout.Space(25);
            DrawDefaultInspector(); //Default selector
            GUILayout.EndVertical();

        }
    }

}
