using System.IO;
using DS.Utilities;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace DS.Windows
{
    using System;
    using Utilites;
    
    public class DSEditorWindow : EditorWindow
    {
        private DSGraphView graphView;
        
        private readonly string defaultFileName = "DialoguesFileName";
        private static TextField fileNameTextField;
        
        private Button saveButton;
        private Button miniMapButton;
        
        [MenuItem("Window/DS/Dialogue Graph")]
        public static void Open()
        {
           GetWindow<DSEditorWindow>("Dialogue Graph");
        }

        private void CreateGUI()
        {
            AddGraphView();

            AddToolbar();
            
            AddStyles();
        }

        #region Elements Addition
        private void AddGraphView()
        {
            graphView = new DSGraphView(this);
            
            graphView.StretchToParentSize();
            
            rootVisualElement.Add(graphView);
        }

        private void AddToolbar()
        {
            Toolbar toolbar = new Toolbar();

            fileNameTextField = DSElementUtility.CreateTextField(defaultFileName, "File Name:", callback =>
            {
                fileNameTextField.value = callback.newValue.RemoveWhitespaces().RemoveSpecialCharacters();
            });

            saveButton = DSElementUtility.CreateButton("Save", () => Save());

            Button loadButton = DSElementUtility.CreateButton("Load", () => Load());
            Button clearButton = DSElementUtility.CreateButton("Clear", () => Clear());
            Button resetButton = DSElementUtility.CreateButton("Reset", () => ResetGraph());
            miniMapButton = DSElementUtility.CreateButton("Minimap", () => ToggleMiniMap());
            
            toolbar.Add(fileNameTextField);
            toolbar.Add(saveButton);
            toolbar.Add(loadButton);
            toolbar.Add(clearButton);
            toolbar.Add(resetButton);
            toolbar.Add(miniMapButton);
            
            toolbar.AddStyleSheets("DialogueSystem/DSToolbarStyles.uss");
            
            rootVisualElement.Add(toolbar);
        }

        private void AddStyles()
        {
            rootVisualElement.AddStyleSheets("DialogueSystem/DSVariables.uss");
        }
        #endregion

        #region Toolbar Actions
        private void Save()
        {
            if (string.IsNullOrEmpty(fileNameTextField.value))
            {
                EditorUtility.DisplayDialog(
                    "Invalid file name.",
                    "Please ensure the file name is valid.",
                    "OK"
                );

                return;
            }

            DSIOUtility.Initialize(graphView, fileNameTextField.value);
            DSIOUtility.Save();
        }
        
        private void Load()
        {
            string filePath = EditorUtility.OpenFilePanel("Dialogue Graphs", "Assets/Editor/DialogueSystem/Graphs", "asset");

            if (string.IsNullOrEmpty(filePath))
            {
                return;
            }

            Clear();
            
            DSIOUtility.Initialize(graphView, Path.GetFileNameWithoutExtension(filePath));
            DSIOUtility.Load();
        }
        
        private void Clear()
        {
            graphView.ClearGraph();
        }
        
        private void ResetGraph()
        {
            Clear();

            UpdateFileName(defaultFileName);
        }
        
        private void ToggleMiniMap()
        {
            graphView.ToggleMiniMap();
            
            miniMapButton.ToggleInClassList("ds-toolbar_button_selected");
        }
        #endregion
        
        #region Utility Methods
        public static void UpdateFileName(string fileName)
        {
            fileNameTextField.value = fileName;
        }
        
        public void EnableSaving()
        {
            saveButton.SetEnabled(true);
        }

        public void DisableSaving()
        {
            saveButton.SetEnabled(false);
        }

        #endregion
    }
}
