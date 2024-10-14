using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace DialogueSystem.Editor
{
    public class DSEditorWindow : EditorWindow
    {
        private DSGraphView graphView;

        private const string defaultFileName = "DialoguesFileName";

        private TextField fileNameTextField;
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
        }

        private void AddGraphView()
        {
            graphView = new DSGraphView(this);
            graphView.StretchToParentSize();
            rootVisualElement.Add(graphView);
            graphView.globalCounterErrors.StateErrorChange += ActiveSaveButton;
        }

        private void ActiveSaveButton(bool IsError)
        {
            saveAsButton.SetEnabled(!IsError);
        }

        private void AddToolbar()
        {
            Toolbar toolbar = new();

            fileNameTextField = DSElementUtility.CreateTextField(defaultFileName, "File Name:", callback =>
            {
                fileNameTextField.value = callback.newValue.RemoveWhitespaces().RemoveSpecialCharacters();
            });

            saveAsButton = DSElementUtility.CreateButton("SaveAs", SaveAs);

            Button loadButton = DSElementUtility.CreateButton("Load", Load);

            Button clearButton = DSElementUtility.CreateButton("Clear", Clear);
            Button resetButton = DSElementUtility.CreateButton("Reset", ResetGraph);
            miniMapButton = DSElementUtility.CreateButton("Minimap", ToggleMiniMap);

            toolbar.Add(fileNameTextField);
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

        private void SaveAs()
        {
            var pathFolder = EditorUtility.OpenFolderPanel("Save", Application.dataPath, "");
            string relativePath = "Assets" + pathFolder.Substring(Application.dataPath.Length);
            graphView.Save(relativePath, fileNameTextField.value);
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
            graphView.Load(graphData);
        }

        public void Load(DSGraphSaveDataSO graphData)
        {
            ResetGraph();
            fileNameTextField.value = graphData.FileName;
            graphView.Load(graphData);
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