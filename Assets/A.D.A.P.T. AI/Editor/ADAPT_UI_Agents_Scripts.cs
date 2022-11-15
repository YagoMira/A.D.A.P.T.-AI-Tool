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

        private void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            Agent a = target as Agent;

            if (state == PlayModeStateChange.EnteredPlayMode || state == PlayModeStateChange.ExitingPlayMode)
            {
                if (EditorWindow.HasOpenInstances<ADAPT_UI_ActionTree>())
                {
                    ADAPT_UI_ActionTree window_actionTree = (ADAPT_UI_ActionTree)EditorWindow.GetWindow(typeof(ADAPT_UI_ActionTree), false);
                    ADAPT_UI_ActionTree.SetAgent(window_actionTree, a);
                    ADAPT_UI_ActionTree.SetAllAgentActions(a);
                    //window_actionTree.Repaint()
                }
            }
        }

        private void OnEnable()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private void OnDisable()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        }

        public override void OnInspectorGUI()
        {
            Agent a = target as Agent;

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
            //DrawDefaultInspector(); //Default selector
            //Exclude the Animation variables and draw the rest:
            DrawPropertiesExcluding(serializedObject, new string[] {"hasAnimations", "onSiteIdle", "onWalkingIdle", "transitionTime"});

            EditorGUILayout.PropertyField(serializedObject.FindProperty("hasAnimations"));
            if (a.hasAnimations) //In case of have some animation...
            {
                EditorGUI.indentLevel += 1;
                EditorGUILayout.PropertyField(serializedObject.FindProperty("transitionTime"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("onSiteIdle"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("onWalkingIdle"));
                EditorGUI.indentLevel -= 1;
            }
            serializedObject.ApplyModifiedProperties();
            GUILayout.EndVertical();

            GUILayout.Space(20);

            /******ACTION TREE UI******/
            GUILayout.BeginHorizontal();
            GUI.backgroundColor = new Color(0f, 255f, 255f); //Color for button
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Show Action Tree", GUILayout.MinWidth(50), GUILayout.MaxWidth(150))) //On Click, open the Action tree window.
            {
                ADAPT_UI_ActionTree window_actionTree = (ADAPT_UI_ActionTree)EditorWindow.GetWindow(typeof(ADAPT_UI_ActionTree), false);
                ADAPT_UI_ActionTree.SetAgent(window_actionTree, a);
                window_actionTree.Show();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            /******ACTION TREE UI******/
        }
        
    }

}
