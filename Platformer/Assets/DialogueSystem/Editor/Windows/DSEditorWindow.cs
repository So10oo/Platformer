using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace DialogueSystem.Editor
{
    public class DSEditorWindow : EditorWindow
    {
        private DSGraphView graphView;
        private SaveLoadService saveLoadService;

        private const string defaultFileName = "DialoguesFileName";

        private TextField fileNameTextField;
        private Button saveButton;
        private Button saveAsButton;
        private Button miniMapButton;

        [MenuItem("Window/DS/Dialogue Graph")]
        public static DSEditorWindow Open()
        {
            return GetWindow<DSEditorWindow>("Dialogue Graph");
        }

        private void OnEnable()
        {
            AddGraphView();
            AddToolbar();
            AddStyles();
            CreateSaveLoadSystem();
        }

        private void AddGraphView()
        {
            graphView = new DSGraphView(this);
            graphView.StretchToParentSize();
            rootVisualElement.Add(graphView);
            graphView.errorController.globalCounterErrors.StateErrorChange += ActiveSaveButton;
        }

        private void ActiveSaveButton(bool IsError)
        {
            saveAsButton.SetEnabled(!IsError);
            saveButton.SetEnabled(!IsError);
        }

        private void AddToolbar()
        {
            Toolbar toolbar = new();

            fileNameTextField = DSElementUtility.CreateTextField(defaultFileName, "File Name:", callback =>
            {
                fileNameTextField.value = callback.newValue.RemoveWhitespaces().RemoveSpecialCharacters();
            });

            saveAsButton = DSElementUtility.CreateButton("Save As", SaveAs);

            saveButton = DSElementUtility.CreateButton("Save", Save);

            Button loadButton = DSElementUtility.CreateButton("Load", Load);
            Button clearButton = DSElementUtility.CreateButton("Clear", Clear);
            Button resetButton = DSElementUtility.CreateButton("Reset", ResetGraph);
            miniMapButton = DSElementUtility.CreateButton("Minimap", ToggleMiniMap);

            toolbar.Add(fileNameTextField);
            toolbar.Add(saveButton);
            toolbar.Add(saveAsButton);
            toolbar.Add(loadButton);
            toolbar.Add(clearButton);
            toolbar.Add(resetButton);
            toolbar.Add(miniMapButton);
            toolbar.AddStyleSheets(PathToStyle.DSToolbarStyles);
            rootVisualElement.Add(toolbar);
        }

        private void AddStyles()
        {
            rootVisualElement.AddStyleSheets(PathToStyle.DSVariables);
        }

        private void CreateSaveLoadSystem()
        {
            saveLoadService = new();
            saveLoadService.OnFailedSave += SaveAs;
        }

        private void Save() => saveLoadService.Save(graphView, fileNameTextField.value);
        
        private void SaveAs()
        {
            var pathFolder = EditorUtility.OpenFolderPanel("Save", Application.dataPath, "");
            if (!string.IsNullOrEmpty(pathFolder))
            {
                string relativePath = "Assets" + pathFolder.Substring(Application.dataPath.Length);
                saveLoadService.SaveAs(graphView, relativePath, fileNameTextField.value);
            } 
        }

        private void Load()
        {
            var fullPath = EditorUtility.OpenFilePanel("Dialogue Graphs Load", Application.dataPath, "asset");

            if (string.IsNullOrEmpty(fullPath))
            {
                Debug.Log("File was not selected");
                return;
            }
            string relativePath = "Assets" + fullPath.Substring(Application.dataPath.Length);
            var graphData = AssetDatabase.LoadAssetAtPath<DSGraphSaveDataSO>(relativePath);
            if (graphData == null)
            {
                Debug.LogWarning("Selected file is not a graph");
                return;
            }
            ResetGraph();
            fileNameTextField.value = graphData.FileName;
            //graphView.Load(graphData);
            saveLoadService.Load(relativePath, graphView, graphData);
        }

        public void Load(DSGraphSaveDataSO graphData)
        {
            ResetGraph();
            fileNameTextField.value = graphData.FileName;
            var path = AssetDatabase.GetAssetPath(graphData);
            //Debug.Log(path);
            saveLoadService.Load(path, graphView, graphData);
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
            miniMapButton.ToggleInClassList("ds-toolbar__button__selected");
        }

        private void UpdateFileName(string newFileName)
        {
            fileNameTextField.value = newFileName;
        }

    }
}