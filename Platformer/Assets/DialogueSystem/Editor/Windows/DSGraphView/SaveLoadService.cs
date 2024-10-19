using DialogSystem.Editor;
using DialogueSystem.Realtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace DialogueSystem.Editor
{
    public class SaveLoadService
    {
        string pathToCurrentFile;

        #region Save
        DSGraphSaveDataSO graphData;
        DSDialogueContainerSO dialogueContainer;

        List<DSNode> nodes;
        List<DSGroup> groups;
        List<CharacterField> characters;

        Dictionary<string, DSDialogueGroupSO> createdGroups;
        Dictionary<string, DSDialogueSO> createdDialogues;
        Dictionary<string, CharacterDataSO> createdCharacters;

        public event Action OnFailedSave;
        public void Save(DSGraphView graph, string name)
        {
            if (pathToCurrentFile != null && File.Exists(pathToCurrentFile))
            {
                AssetDatabase.DeleteAsset(pathToCurrentFile);
                var relativePath = Path.GetDirectoryName(pathToCurrentFile);
                pathToCurrentFile = $"{relativePath}/{name}DialogueContainer.asset";
                BaseSave(graph, pathToCurrentFile, name);
            }
            else
                OnFailedSave?.Invoke();
        }

        public void SaveAs(DSGraphView graph, string path, string name)
        {
            pathToCurrentFile = $"{path}/{name}DialogueContainer.asset";
            BaseSave(graph, pathToCurrentFile, name);
        }

        private void BaseSave(DSGraphView graph, string pathToSave, string name)
        {
            //берем все элемент из графа
            nodes = GetElementsForGraph<DSNode>(graph.graphElements);
            groups = GetElementsForGraph<DSGroup>(graph.graphElements);
            characters = graph.characters;

            //создаем два объекта один для графа воссоздания графа другой для хранения диалогов
            graphData = DSGraphSaveDataSO.CreateInstance();
            dialogueContainer = DSDialogueContainerSO.CreateInstance();
            graphData.FileName = name;

            createdGroups = new();
            createdDialogues = new();
            createdCharacters = new();
            SaveGroups();
            SaveCharacters();
            SaveNodes();

            AssetDatabase.CreateAsset(dialogueContainer, pathToSave);
            graphData.name = $"{name}Graph";
            AssetDatabase.AddObjectToAsset(graphData, dialogueContainer);

            foreach (var character in createdCharacters.Values)
            {
                EditorUtility.SetDirty(character);
                AssetDatabase.AddObjectToAsset(character, dialogueContainer);
            }

            foreach (var dialogues in createdDialogues.Values)
            {
                EditorUtility.SetDirty(dialogues);
                AssetDatabase.AddObjectToAsset(dialogues, dialogueContainer);
            }

            EditorUtility.SetDirty(graphData);
            EditorUtility.SetDirty(dialogueContainer);
            AssetDatabase.SaveAssets();
        }

        #region SavaGroups
        private void SaveGroups()
        {
            foreach (DSGroup group in groups)
            {
                SaveGroupToGraph(group);
                SaveGroupToContainer(group);
            }
        }
        private void SaveGroupToContainer(DSGroup group)
        {
            var dialogueGroup = new DSDialogueGroupSO(group);
            dialogueContainer.DialogueGroups.Add(dialogueGroup, new List<DSDialogueSO>());
            createdGroups.Add(group.ID, dialogueGroup);
        }
        private void SaveGroupToGraph(DSGroup group)
        {
            graphData.Groups.Add(new DSGroupSaveData(group));
        }
        #endregion

        #region SavaNodes
        private void SaveNodes()
        {
            foreach (DSNode node in nodes)
            {
                SaveNodeToGraph(node);
                SaveNodeToContainer(node);
            }
            UpdateDialoguesChoicesConnections();
        }
        private void SaveNodeToGraph(DSNode node)
        {
            graphData.Nodes.Add(new DSNodeSaveData(node));
        }
        private void SaveNodeToContainer(DSNode node)
        {
            DSDialogueSO dialogue = DSDialogueSO.CreateInstance(node, createdCharacters);
            if (node.Group != null)
            {
                var dialogGroups = createdGroups[node.Group.ID];
                dialogueContainer.DialogueGroups.AddItem(dialogGroups, dialogue);
            }
            else
                dialogueContainer.UngroupedDialogues.Add(dialogue);
            createdDialogues.Add(node.ID, dialogue);
        }
        private void UpdateDialoguesChoicesConnections()
        {
            foreach (DSNode node in nodes)
            {
                DSDialogueSO dialogue = createdDialogues[node.ID];

                for (int choiceIndex = 0; choiceIndex < node.Choices.Count; ++choiceIndex)
                {
                    DSChoiceSaveData nodeChoice = node.Choices[choiceIndex];
                    if (string.IsNullOrEmpty(nodeChoice.NodeID))
                        continue;
                    dialogue.Choices[choiceIndex].NextDialogue = createdDialogues[nodeChoice.NodeID];
                }
            }
        }
        #endregion

        #region SavaCharacters
        void SaveCharacters()
        {
            foreach (var character in characters)
            {
                var characterData = CharacterDataSO.CreateInstance(character);
                EditorUtility.SetDirty(characterData);
                createdCharacters.Add(character.ID, characterData);
                dialogueContainer.Characters.Add(characterData);
                graphData.Characters.Add(new CharacterSaveData(character));
            }
        }
        #endregion
        public List<T> GetElementsForGraph<T>(UnityEngine.UIElements.UQueryState<GraphElement> graphElements) where T : GraphElement
        {
            List<T> elements = new List<T>();
            foreach (var element in graphElements)
            {
                if (element is T elem)
                    elements.Add(elem);
            }
            return elements;
        }
        #endregion

        #region Load
        Dictionary<string, DSGroup> loadedGroups;
        Dictionary<string, DSNode> loadedNodes;

        public void Load(string pathToFile, DSGraphView graphView, DSGraphSaveDataSO graphData)
        {
            try
            {
                pathToCurrentFile = pathToFile;
                loadedGroups = new();
                loadedNodes = new();
                LoadGroups(graphView, graphData.Groups);
                LoadCharacters(graphView, graphData.Characters);
                LoadNodes(graphView, graphData.Nodes);
                LoadNodesConnections(graphView);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        private void LoadCharacters(DSGraphView graphView, List<CharacterSaveData> characters)
        {
            foreach (var character in characters)
            {
                var cf = new CharacterField(character.Name, character.Icon, character.ID);
                graphView.AddCharacterField(cf);
            }
        }

        private void LoadGroups(DSGraphView graphView, List<DSGroupSaveData>  groups)
        {
            foreach (DSGroupSaveData groupData in groups)
            {
                var group = graphView.LoadGroup(groupData.Name, groupData.Position, groupData.ID);
                loadedGroups.Add(groupData.ID, group);
            }
        }

        private void LoadNodes(DSGraphView graphView, List<DSNodeSaveData> nodes)
        {
            foreach (DSNodeSaveData nodeData in nodes)
            {
                var node = graphView.LoadNode(nodeData, graphView.characters);
                loadedNodes.Add(node.ID, node);
                if (string.IsNullOrEmpty(nodeData.GroupID))
                    continue;
                DSGroup group = loadedGroups[nodeData.GroupID];
                node.Group = group;
                group.AddElement(node);
            }
        }

        private void LoadNodesConnections(DSGraphView graphView)
        {
            foreach (KeyValuePair<string, DSNode> loadedNode in loadedNodes)
            {
                foreach (Port choicePort in loadedNode.Value.outputContainer.Children())
                {
                    DSChoiceSaveData choiceData = (DSChoiceSaveData)choicePort.userData;
                    if (string.IsNullOrEmpty(choiceData.NodeID))
                        continue;
                    DSNode nextNode = loadedNodes[choiceData.NodeID];
                    Port nextNodeInputPort = (Port)nextNode.inputContainer.Children().First();
                    Edge edge = choicePort.ConnectTo(nextNodeInputPort);
                    graphView.AddElement(edge);
                    loadedNode.Value.RefreshPorts();
                }
            }
        }
        #endregion
    }
}
