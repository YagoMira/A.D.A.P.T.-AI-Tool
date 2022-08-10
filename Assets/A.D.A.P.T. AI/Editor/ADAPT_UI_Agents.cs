using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.IO;
using System;

namespace ADAPT.UI
{
    public class ADAPT_UI_Agents : EditorWindow
    {
        protected Texture2D adapt_logo;
        protected ADAPT_UI_Resources adapt_resources;
        protected ObjectField selectedObject;
        string custom_actions_path = "Assets/A.D.A.P.T. AI/Scripts/Custom/"; //DON'T CHANGE!!!

        [MenuItem("Tools/A.D.A.P.T.")]
        public static void ShowWindow()
        {
            ADAPT_UI_Agents window = GetWindow<ADAPT_UI_Agents>();
            window.titleContent = new GUIContent("A.D.A.P.T.");
            window.minSize = new Vector2(300, 350);
            window.maxSize = new Vector2(400, 510);
        }

        public void InitializeFiles(VisualElement root)
        {
            //Initialize String,Path, ... resources
            adapt_resources = new ADAPT_UI_Resources();
            //Import Logo from path
            adapt_logo = Resources.Load<Texture2D>(adapt_resources.logo_path);

            // Import UXML
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/A.D.A.P.T. AI/Editor/ADAPT_UI_Agents.uxml");
            VisualElement uxmlRoot = visualTree.CloneTree();
            root.Add(uxmlRoot);

            // A stylesheet can be added to a VisualElement.
            // The style will be applied to the VisualElement and all of its children.
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/A.D.A.P.T. AI/Editor/ADAPT_UI_Agents.uss");
            root.styleSheets.Add(styleSheet);
        }

        public void CreateGUI()
        {
            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;

            InitializeFiles(root);

            // Set the container height to the window
            rootVisualElement.Q<VisualElement>("MainContainer").style.height = new
                StyleLength(position.height);

            
            rootVisualElement.Q<VisualElement>("ImageHolder").style.backgroundImage = new StyleBackground(adapt_logo);

            //AGENTS - Subsection
            rootVisualElement.Q<Label>("AgentsLabel").text = adapt_resources.agents_text;
            rootVisualElement.Q<Label>("DescriptionLabel").text = adapt_resources.agent_description_text;
            rootVisualElement.Q<Label>("AgentSelectionLabel").text = adapt_resources.selected_gameobject_text;
            rootVisualElement.Q<Label>("AgentExistsLabel").text = adapt_resources.agent_exists_text;
            rootVisualElement.Q<Label>("AgentExistsLabel").style.display = DisplayStyle.None;
            rootVisualElement.Q<Button>("AgentButton").text = adapt_resources.agent_button;
            rootVisualElement.Q<Button>("AgentButton").SetEnabled(false);
            rootVisualElement.Q<Button>("AgentButton").clicked += AddAgentScript;
            selectedObject = root.Q<ObjectField>("SelectedObjectHolder");
            selectedObject.objectType = typeof(GameObject);

            //ACTIONS - Subsection
            rootVisualElement.Q<Label>("ActionsLabel").text = adapt_resources.actions_text;
            rootVisualElement.Q<Label>("DescriptionActionLabel").text = adapt_resources.action_description_text;
            rootVisualElement.Q<Label>("ActionExistsLabel").text = adapt_resources.action_not_exists_text;
            rootVisualElement.Q<Label>("ActionFileExistsLabel").text = adapt_resources.action_file_exists_text;
            rootVisualElement.Q<Label>("ActionExistsLabel").style.display = DisplayStyle.None;
            rootVisualElement.Q<Label>("ActionFileExistsLabel").style.display = DisplayStyle.None;
            rootVisualElement.Q<Button>("ActionButton").text = adapt_resources.action_button;
            rootVisualElement.Q<Button>("ActionButton").SetEnabled(false);
            rootVisualElement.Q<Button>("ActionButton").clicked += AddActionScript;
            rootVisualElement.Q<Label>("ActionNameLabel").text = adapt_resources.action_name_text;
        }

        private void OnSelectionChange()
        {
            //Hidde text if any gameobject is selected
            rootVisualElement.Q<Label>("DescriptionLabel").style.display = DisplayStyle.None;
            rootVisualElement.Q<Label>("DescriptionActionLabel").style.display = DisplayStyle.None;
            rootVisualElement.Q<VisualElement>("AgentSelectionHolder").style.display = DisplayStyle.Flex;
            rootVisualElement.Q<VisualElement>("ActionNameContainer").style.display = DisplayStyle.Flex;

            if (Selection.objects.Length <= 0) //If GameObject is NOT selected
            {
                rootVisualElement.Q<Label>("DescriptionLabel").style.display = DisplayStyle.Flex;
                rootVisualElement.Q<Label>("DescriptionActionLabel").style.display = DisplayStyle.Flex;
                rootVisualElement.Q<VisualElement>("AgentSelectionHolder").style.display = DisplayStyle.None;
                rootVisualElement.Q<VisualElement>("ActionNameContainer").style.display = DisplayStyle.None;

                selectedObject.value = null;
                rootVisualElement.Q<Button>("AgentButton").SetEnabled(false);
                rootVisualElement.Q<Button>("ActionButton").SetEnabled(false);
            }
            else //If GameObject is selected
            {
                selectedObject.value = Selection.objects[0]; //Select only the first GameObject to become in to an Agent
                rootVisualElement.Q<Button>("AgentButton").SetEnabled(true);
                rootVisualElement.Q<Button>("ActionButton").SetEnabled(true);
            }

            //Dissapear warning label texts
            if(rootVisualElement.Q<Label>("AgentExistsLabel").style.display == DisplayStyle.Flex)
                rootVisualElement.Q<Label>("AgentExistsLabel").style.display = DisplayStyle.None;

            if (rootVisualElement.Q<Label>("ActionExistsLabel").style.display == DisplayStyle.Flex)
                rootVisualElement.Q<Label>("ActionExistsLabel").style.display = DisplayStyle.None;

            if (rootVisualElement.Q<Label>("ActionFileExistsLabel").style.display == DisplayStyle.Flex)
                rootVisualElement.Q<Label>("ActionFileExistsLabel").style.display = DisplayStyle.None;
        }

        private void AddAgentScript() //When Button is clicked add the Agent.cs script to the actual selected GameObject
        {
            GameObject actualSelection = (GameObject)Selection.objects[0];
            //Check if actual selected GameObject is an Agent
            if (actualSelection.GetComponents<Agent>().Length >= 1)
            {
                rootVisualElement.Q<Label>("AgentExistsLabel").style.display = DisplayStyle.Flex;
            }
            else
            {
                rootVisualElement.Q<Label>("AgentExistsLabel").style.display = DisplayStyle.None;
                ((GameObject)Selection.objects[0]).AddComponent<Agent>();
            }

        }
        private void AddActionScript() //When Button is clicked add the Action.cs('Custom Named Action.cs') script to the actual selected GameObject
        {
            GameObject actualSelection = (GameObject)Selection.objects[0];
            string script_name, path;
            //Check if actual selected GameObject is an Agent
            if (actualSelection.GetComponents<Agent>().Length >= 1)
            {
                script_name = rootVisualElement.Q<TextField>("ActionName").value;
                rootVisualElement.Q<Label>("ActionExistsLabel").style.display = DisplayStyle.None;
                rootVisualElement.Q<Label>("ActionFileExistsLabel").style.display = DisplayStyle.None;
                path = custom_actions_path + script_name + ".cs"; //DON'T CHANGE THE PATH!.

                if (File.Exists(path))
                {
                    rootVisualElement.Q<Label>("ActionFileExistsLabel").style.display = DisplayStyle.Flex;
                    /*****
                        //Debug.Log("!!!!!!!!! : " + Activator.CreateInstance("Assembly-CSharp", "D").Unwrap().GetType().ToString());
                        //((GameObject)Selection.objects[0]).AddComponent(path);
                    *****/
                }
                else
                {
                    rootVisualElement.Q<Label>("ActionFileExistsLabel").style.display = DisplayStyle.None;

                    //Create the new action script
                    StreamWriter writer = new StreamWriter(path, true);
                    writer.WriteLine("using UnityEngine;");
                    writer.WriteLine("public class " + script_name + " : Action {}");
                    writer.Close();

                    /*****
                        //Refresh DB of Unity Assets:      AssetDatabase.Refresh( ImportAssetDatabase.LoadSync );
                        //Add Component to the actual Agent.
                        //((GameObject)Selection.objects[0]).AddComponent(Type.GetType(script_name));
                        //var type = (Type.GetType(script_name));
                    *****/

                }
            }
            else
            {
                rootVisualElement.Q<Label>("ActionExistsLabel").style.display = DisplayStyle.Flex;
                rootVisualElement.Q<Label>("ActionFileExistsLabel").style.display = DisplayStyle.None;
            }

        }

    }
}
