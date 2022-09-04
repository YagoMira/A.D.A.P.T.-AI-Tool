using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.IO;
using System;
using System.Reflection;

namespace ADAPT.UI
{ 
    public class ADAPT_UI_Agents : EditorWindow
    {
        protected Texture2D adapt_logo;
        protected ADAPT_UI_Resources adapt_resources;
        protected ObjectField selectedObject;
        string custom_scripts_path = "Assets/A.D.A.P.T. AI/Scripts/Custom/"; //DON'T CHANGE!!!

        [MenuItem("Tools/A.D.A.P.T./Menu")]
        public static void ShowWindow()
        {
            ADAPT_UI_Agents window = GetWindow<ADAPT_UI_Agents>();
            window.titleContent = new GUIContent("A.D.A.P.T.");
            window.minSize = new Vector2(370, 510);
            window.maxSize = new Vector2(420, 580);
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
            rootVisualElement.Q<Label>("AgentScriptExistsLabel").text = adapt_resources.agent_script_exists_text;
            rootVisualElement.Q<Label>("AgentScriptExistsLabel").style.display = DisplayStyle.None;
            rootVisualElement.Q<Button>("AgentButton").text = adapt_resources.agent_button;
            rootVisualElement.Q<Button>("AgentButton").SetEnabled(false);
            rootVisualElement.Q<Button>("AgentButton").clicked += AddAgentScript;
            rootVisualElement.Q<Label>("AgentNameLabel").text = adapt_resources.agent_name_text;
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
            rootVisualElement.Q<VisualElement>("AgentNameContainer").style.display = DisplayStyle.Flex;
            rootVisualElement.Q<VisualElement>("ActionNameContainer").style.display = DisplayStyle.Flex;

            if (Selection.objects.Length <= 0) //If GameObject is NOT selected
            {
                rootVisualElement.Q<Label>("DescriptionLabel").style.display = DisplayStyle.Flex;
                rootVisualElement.Q<Label>("DescriptionActionLabel").style.display = DisplayStyle.Flex;
                rootVisualElement.Q<VisualElement>("AgentSelectionHolder").style.display = DisplayStyle.None;
                rootVisualElement.Q<VisualElement>("AgentNameContainer").style.display = DisplayStyle.None;
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

            if (rootVisualElement.Q<Label>("AgentScriptExistsLabel").style.display == DisplayStyle.Flex)
                rootVisualElement.Q<Label>("AgentScriptExistsLabel").style.display = DisplayStyle.None;
    }

        private void AddAgentScript() //When Button is clicked add the Agent.cs script to the actual selected GameObject
        {

            GameObject actualSelection = (GameObject)Selection.objects[0];
            if(actualSelection == null)
            {
                Debug.Log("<color=red>GAMEOBJECT IS NOT SELECTED!.</color>");
            }

            string script_name, path;

            //Check if actual selected GameObject is an Agent
            if (actualSelection.GetComponents<Agent>().Length >= 1)
            {
                rootVisualElement.Q<Label>("AgentExistsLabel").style.display = DisplayStyle.Flex;
            }
            else
            {
                script_name = rootVisualElement.Q<TextField>("AgentName").value;
                rootVisualElement.Q<Label>("AgentExistsLabel").style.display = DisplayStyle.None;
                path = custom_scripts_path + script_name + ".cs"; //DON'T CHANGE THE PATH!.

                if(File.Exists(path)) //If the current file exists
                {
                    rootVisualElement.Q<Label>("AgentScriptExistsLabel").style.display = DisplayStyle.Flex;
                }
                else //Create NEW SCRIPT AND ADD IT!.
                {
                    WriteFile(0, path, script_name);
            }
            }

        }

        private void AddActionScript() //When Button is clicked add the Action.cs('Custom Named Action.cs') script to the actual selected GameObject
        {
            GameObject actualSelection = (GameObject)Selection.objects[0];
            if (actualSelection == null)
            {
                Debug.Log("<color=red>GAMEOBJECT IS NOT SELECTED!.</color>");
            }

            string script_name, path;

            //Check if actual selected GameObject is an Agent
            if (actualSelection.GetComponents<Agent>().Length >= 1)
            {
                script_name = rootVisualElement.Q<TextField>("ActionName").value;
                rootVisualElement.Q<Label>("ActionExistsLabel").style.display = DisplayStyle.None;
                rootVisualElement.Q<Label>("ActionFileExistsLabel").style.display = DisplayStyle.None;
                path = custom_scripts_path + script_name + ".cs"; //DON'T CHANGE THE PATH!.

                if (File.Exists(path))
                {
                    rootVisualElement.Q<Label>("ActionFileExistsLabel").style.display = DisplayStyle.Flex;
                }
                else
                {
                    rootVisualElement.Q<Label>("ActionFileExistsLabel").style.display = DisplayStyle.None;
                    WriteFile(1, path, script_name);
                }
            }
            else
            {
                rootVisualElement.Q<Label>("ActionExistsLabel").style.display = DisplayStyle.Flex;
                rootVisualElement.Q<Label>("ActionFileExistsLabel").style.display = DisplayStyle.None;
            }
        }

        [UnityEditor.Callbacks.DidReloadScripts]
        private static void ScriptReloaded()
        {
            int flag = 0;  //FLAG == 1 : Write Agent file | FLAG == 2 : Write Action file.
            
            //Check if key exists.
            if (EditorPrefs.HasKey("SavedAgent"))
            {
                flag = 1;
            }
            else if(EditorPrefs.HasKey("SavedAction"))
            {
                flag = 2;
            }
            else
            {
                flag = 0;
                return;
            }

            
            string name = "";

            if(flag == 1)
            {
                name = EditorPrefs.GetString("SavedAgent");
            }
            else if(flag == 2)
            {
                name = EditorPrefs.GetString("SavedAction");
            }

            GameObject agent = (GameObject)Selection.objects[0];

            if (agent == null)
            {
                return;
            }

            //Get the new type from the new script from reloaded assembly.z
            Type type = Activator.CreateInstance("Assembly-CSharp", name).Unwrap().GetType();
            agent.AddComponent(type);

            //Delete the keys after used.
            if (flag == 1)
            {
                EditorPrefs.DeleteKey("SavedAgent");
            }
            else if (flag == 2)
            {
                EditorPrefs.DeleteKey("SavedAction");
            }
        }

        private void WriteFile(int flag, string path, string script_name) //FLAG == 0 : Write Agent file | FLAG == 1 : Write Action file.
        {
            StreamWriter writer = new StreamWriter(path, true);
            string to_save = ""; //String to have a reference when reloading scripts.

            if(flag == 0) //Agent file
            {
                writer.WriteLine("using UnityEngine;");
                writer.WriteLine("public class " + script_name + " : Agent \n{");
                writer.WriteLine("new void Start() //DON'T MODIFY ANY LINE OF THIS FUNCTION!!!\n {");
                writer.WriteLine("AddGoals();");
                writer.WriteLine("/************/");
                writer.WriteLine("base.Start(); //DON'T DELETE THIS LINE!!!");
                writer.WriteLine("/************/");
                writer.WriteLine(" ManageStates();\n }");
                writer.WriteLine("\n public void AddGoals() {}");
                writer.WriteLine("\n public void ManageStates() {}\n}");
                writer.Close();

                to_save = "SavedAgent";
            }
            else if (flag == 1) //Action file
            {
                writer.WriteLine("using UnityEngine;");
                writer.WriteLine("using UnityEngine.AI;");
                writer.WriteLine("public class " + script_name + " : Action \n{");
                writer.WriteLine("string a_name = \"ActionName\";");
                writer.WriteLine("Agent agent;");
                writer.WriteLine("NavMeshAgent actual_agent;\n");
                writer.WriteLine("void Awake() \n{ ");
                writer.WriteLine("/******DON'T DELETE THIS LINES!!!******/");
                writer.WriteLine("actionName = a_name;");
                writer.WriteLine("agent = gameObject.GetComponent<Agent>();");
                writer.WriteLine("//WARNING MESSAGE!");
                writer.WriteLine("Debug.Log(\" <color=blue> Action: </color> \" + actionName + \" <color=blue> has preconditions / effects added by code,</ color > <color=red> DON'T ADD MORE VIA INSPECTOR!.</color>\");");
                writer.WriteLine("/************/");
                writer.WriteLine("\n/************/");
                writer.WriteLine("//HERE YOU CAN ADD YOUR PRECONDITIONS // EFFECTS");
                writer.WriteLine("/************/");
                writer.WriteLine("//In case of add preconditions/effects, uncomment the next lines:");
                writer.WriteLine("//preconditions_list.Add(GoTo_preconditions);");
                writer.WriteLine("//effects_list.Add(GoTo_effects);\n }");
                writer.WriteLine("");
                writer.WriteLine("public override void PerformAction() \n{ ");
                writer.WriteLine("//Uncomment next line if you need some navmesh:");
                writer.WriteLine("//actual_agent = gameObject.GetComponent<NavMeshAgent>();");
                writer.WriteLine("//Use 'finished = true;' when finish the action.\n}");
                writer.WriteLine("\n}");
                writer.Close();

                to_save = "SavedAction";
            }
            EditorPrefs.SetString(to_save, script_name);

            AssetDatabase.ImportAsset(custom_scripts_path + script_name + ".cs");
            AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport | ImportAssetOptions.ForceUpdate);
        }

    }
}
