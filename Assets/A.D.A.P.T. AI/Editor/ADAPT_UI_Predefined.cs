using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using UnityEngine.UIElements;

namespace ADAPT.UI
{
    public class ADAPT_UI_Predefined : EditorWindow
    {
        protected ADAPT_UI_Resources adapt_resources;
        static string predefined_scripts_path = "Assets/A.D.A.P.T. AI/Scripts/Custom/Predefined"; //DON'T CHANGE!!!
        static List<string> scripts_paths;

        [MenuItem("Tools/A.D.A.P.T./Predefined Actions")]
        public static void ShowWindow()
        {
            ADAPT_UI_Predefined window = GetWindow<ADAPT_UI_Predefined>();
            window.titleContent = new GUIContent("A.D.A.P.T. - Actions");
            window.minSize = new Vector2(400, 510);
            window.maxSize = new Vector2(440, 580);
        }

        public void InitializeFiles(VisualElement root)
        {
            //Initialize String,Path, ... resources
            adapt_resources = new ADAPT_UI_Resources();
            // Import UXML
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/A.D.A.P.T. AI/Editor/ADAPT_UI_Predefined.uxml");
            VisualElement uxmlRoot = visualTree.CloneTree();
            root.Add(uxmlRoot);

            // A stylesheet can be added to a VisualElement.
            // The style will be applied to the VisualElement and all of its children.
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/A.D.A.P.T. AI/Editor/ADAPT_UI_Predefined.uss");
            root.styleSheets.Add(styleSheet);
        }

        public void CreateGUI()
        {
            scripts_paths = new List<string>();

            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;

            InitializeFiles(root);

            // Set the container height to the window
            rootVisualElement.Q<VisualElement>("MainContainer").style.height = new
                StyleLength(position.height);

            rootVisualElement.Q<Label>("DescriptionLabel").text = adapt_resources.description_text;
            rootVisualElement.Q<Label>("AdviceLabel").text = adapt_resources.action_not_exists_text;
            rootVisualElement.Q<Label>("AdviceLabel").style.display = DisplayStyle.None;
            rootVisualElement.Q<Label>("AdviceActionLabel").text = adapt_resources.action_file_added_text;
            rootVisualElement.Q<Label>("AdviceActionLabel").style.display = DisplayStyle.None;

            LoadPathsRecursive(predefined_scripts_path);

            var filesList = new ListView();

            filesList.style.height = Length.Percent(100);
            filesList.style.width = Length.Percent(100);

            rootVisualElement.Q<VisualElement>("FolderContainer").Add(filesList);

            filesList.makeItem = () =>
            {
                var container = new VisualElement();
                container.style.height = 5;
                container.style.marginBottom = 5;
                container.style.flexDirection = FlexDirection.Row;
                container.style.alignContent = Align.Center;
                container.style.justifyContent = Justify.Center;
                container.style.paddingTop = Length.Percent(2);

                var label_1 = new Label();
                label_1.name = "GENRE";
                label_1.style.height = Length.Percent(50);
                label_1.style.width = Length.Percent(25);
                label_1.style.unityFontStyleAndWeight = FontStyle.Bold;
                label_1.style.color = Color.red;

                var label_2 = new Label();
                label_2.name = "ACTION";
                label_2.style.height = Length.Percent(50);
                label_2.style.width = Length.Percent(30);
                label_2.style.unityFontStyleAndWeight = FontStyle.Bold;

                var button = new Button();
                button.style.height = Length.Percent(52);
                button.style.width = Length.Percent(15);

                container.Add(label_1);
                container.Add(label_2);
                container.Add(button);

                return container;
            };

            filesList.bindItem = (e, i) =>
            {
                var label_1 = e.Q<Label>("GENRE");
                var label_2 = e.Q<Label>("ACTION");

                label_1.text = (new DirectoryInfo(scripts_paths[i]).Parent.Name).ToString().ToUpper();
                label_2.text = ":   " + Path.GetFileNameWithoutExtension(scripts_paths[i]);

                var button = e.Q<Button>();
                button.text = " + ";
                button.clicked += () => ButtonEvent(scripts_paths[i]);
            };
            filesList.itemsSource = scripts_paths;
        }

        public void ButtonEvent(string scriptAction)
        {
            string actionType = Path.GetFileNameWithoutExtension(scriptAction);
            GameObject actualSelection = Selection.gameObjects[0];

            if (actualSelection.GetComponents<Agent>().Length >= 1)
            {
                AssetDatabase.ImportAsset(scriptAction);
                AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport | ImportAssetOptions.ForceUpdate);
                Type type = Activator.CreateInstance("Assembly-CSharp", actionType).Unwrap().GetType();

                if (actualSelection.GetComponents(type).Length >= 1)
                {
                    rootVisualElement.Q<Label>("AdviceActionLabel").style.display = DisplayStyle.Flex;
                    rootVisualElement.Q<Label>("DescriptionLabel").style.display = DisplayStyle.None;
                }
                else
                {
                    rootVisualElement.Q<Label>("AdviceLabel").style.display = DisplayStyle.None;
                    rootVisualElement.Q<Label>("DescriptionLabel").style.display = DisplayStyle.Flex;
                    actualSelection.AddComponent(type);
                }
            }
            else
            {
                rootVisualElement.Q<Label>("AdviceLabel").style.display = DisplayStyle.Flex;
                rootVisualElement.Q<Label>("AdviceActionLabel").style.display = DisplayStyle.None;
                rootVisualElement.Q<Label>("DescriptionLabel").style.display = DisplayStyle.None;
            }
        }

        static void LoadPathsRecursive(string path)
        {

            DirectoryInfo dirInfo = new DirectoryInfo(path);

            if(dirInfo.GetFiles().Length != 0) //If actual dir has some files.
            {
                foreach (var file in dirInfo.GetFiles())
                {
                    if (!file.Name.Contains(".meta"))
                    {
                        scripts_paths.Add(path + file.Name);
                    }
                }
            }

            foreach (var dir in dirInfo.GetDirectories())
            {
                LoadPathsRecursive(predefined_scripts_path + "/" + dir.Name + "/");
            }
        }

        private void OnSelectionChange()
        {
            //Dissapear warning label texts
            if (rootVisualElement.Q<Label>("DescriptionLabel").style.display == DisplayStyle.None)
                rootVisualElement.Q<Label>("DescriptionLabel").style.display = DisplayStyle.Flex;
            if (rootVisualElement.Q<Label>("AdviceLabel").style.display == DisplayStyle.Flex)
                rootVisualElement.Q<Label>("AdviceLabel").style.display = DisplayStyle.None;
            if (rootVisualElement.Q<Label>("AdviceActionLabel").style.display == DisplayStyle.Flex)
                rootVisualElement.Q<Label>("AdviceActionLabel").style.display = DisplayStyle.None; 
        }

    }
}
