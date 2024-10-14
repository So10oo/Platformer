using DialogSystem.Editor;
using DialogueSystem.Realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace DialogueSystem.Editor
{
    public partial class DSGraphView
    {
        string pathToGraph;

        public void Save(string path, string name)
        {
            try
            {
                //берем все элемент из графа
                var (nodes, groups) = GetElementsFromGraphView();

                //создаем два объекта один для графа воссоздания графа другой для хранения диалогов
                DSGraphSaveDataSO graphData = DSGraphSaveDataSO.CreateInstance();
                DSDialogueContainerSO dialogueContainer = DSDialogueContainerSO.CreateInstance();
                graphData.FileName = name;

                var createdGroups = new Dictionary<string, DSDialogueGroupSO>();
                var createdDialogues = new Dictionary<string, DSDialogueSO>();

                SaveGroups(graphData, dialogueContainer, groups, createdGroups);
                SaveNodes(graphData, dialogueContainer, nodes, createdDialogues, createdGroups);

                AssetDatabase.CreateAsset(graphData, $"{path}/{name}Graph.asset");
                AssetDatabase.CreateAsset(dialogueContainer, $"{path}/{name}DialogueContainer.asset");

                foreach (var dialogues in createdDialogues.Values)
                {
                    EditorUtility.SetDirty(dialogues);
                    dialogues.name = dialogues.DialogueName;
                    AssetDatabase.AddObjectToAsset(dialogues, dialogueContainer);
                }

                EditorUtility.SetDirty(graphData);
                EditorUtility.SetDirty(dialogueContainer);

                AssetDatabase.SaveAssets();
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }

        }

        private void SaveGroups(DSGraphSaveDataSO graphData, DSDialogueContainerSO dialogueContainer, List<DSGroup> groups,
            Dictionary<string, DSDialogueGroupSO> createdGroups)
        {
            foreach (DSGroup group in groups)
            {
                SaveGroupToGraph(group, graphData);
                SaveGroupToScriptableObject(group, dialogueContainer, createdGroups);
            }
        }

        private void SaveNodes(DSGraphSaveDataSO graphData, DSDialogueContainerSO dialogueContainer, List<DSNode> nodes,
            Dictionary<string, DSDialogueSO> createdDialogues, Dictionary<string, DSDialogueGroupSO> createdGroups)
        {
            foreach (DSNode node in nodes)
            {
                SaveNodeToGraph(node, graphData);
                SaveNodeToScriptableObject(node, dialogueContainer, createdGroups, createdDialogues);
            }
            UpdateDialoguesChoicesConnections(dialogueContainer, createdDialogues);
        }

        private void UpdateDialoguesChoicesConnections(DSDialogueContainerSO dialogueContainer, Dictionary<string, DSDialogueSO> createdDialogues)
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

        private void SaveNodeToScriptableObject(DSNode node, DSDialogueContainerSO dialogueContainer,
            Dictionary<string, DSDialogueGroupSO> createdGroups, Dictionary<string, DSDialogueSO> createdDialogues)
        {
            DSDialogueSO dialogue = DSDialogueSO.CreateInstance(node);
            if (node.Group != null)
            {
                var dialogGroups = createdGroups[node.Group.ID];
                dialogueContainer.DialogueGroups.AddItem(dialogGroups, dialogue);
            }
            else
                dialogueContainer.UngroupedDialogues.Add(dialogue);
            createdDialogues.Add(node.ID, dialogue);
        }

        private void SaveNodeToGraph(DSNode node, DSGraphSaveDataSO graphData)
        {
            graphData.Nodes.Add(new DSNodeSaveData(node));
        }

        private (List<DSNode>, List<DSGroup>) GetElementsFromGraphView()
        {
            List<DSNode> nodes = new();
            List<DSGroup> groups = new();
            this.graphElements.ForEach(graphElement =>
            {
                switch (graphElement)
                {
                    case DSNode node:
                        nodes.Add(node);
                        break;
                    case DSGroup dsGroup:
                        groups.Add(dsGroup);
                        break;
                    default:
                        break;
                }
            });

            return (nodes, groups);
        }

        private void SaveGroupToGraph(DSGroup group, DSGraphSaveDataSO graphData)
        {
            graphData.Groups.Add(new DSGroupSaveData(group));
        }

        private void SaveGroupToScriptableObject(DSGroup group, DSDialogueContainerSO dialogueContainer, Dictionary<string, DSDialogueGroupSO> createdGroups)
        {
            var dialogueGroup = new DSDialogueGroupSO(group);
            dialogueContainer.DialogueGroups.Add(dialogueGroup, new List<DSDialogueSO>());
            createdGroups.Add(group.ID, dialogueGroup);
        }

        public void Load(DSGraphSaveDataSO graphData)
        {
            try
            {
                Dictionary<string, DSGroup> loadedGroups = new();
                Dictionary<string, DSNode> loadedNodes = new();
                LoadGroups(graphData, loadedGroups);
                LoadNodes(graphData.Nodes, loadedNodes, loadedGroups);
                LoadNodesConnections(loadedNodes);
            }
            catch (Exception e)
            {
                //this.ClearGraph();
                Debug.LogException(e);
            }
             
        }
        public T LoadAsset<T>(string path, string assetName) where T : ScriptableObject
        {
            return AssetDatabase.LoadAssetAtPath<T>($"{path}/{assetName}.asset");
        }

        private void LoadGroups(DSGraphSaveDataSO graphData, Dictionary<string, DSGroup> loadedGroups)
        {
            foreach (DSGroupSaveData groupData in graphData.Groups)
            {
                DSGroup group = this.CreateGroup(groupData.Name, groupData.Position);

                group.ID = groupData.ID;

                loadedGroups.Add(group.ID, group);
                this.AddElement(group);
                OnAddNewGroup?.Invoke(group);
            }
        }

        private void LoadNodes(List<DSNodeSaveData> nodes, Dictionary<string, DSNode> loadedNodes, Dictionary<string, DSGroup> loadedGroups)
        {
            foreach (DSNodeSaveData nodeData in nodes)
            {
                List<DSChoiceSaveData> choices = nodeData.Choices.Clone();

                DSNode node = this.CreateNode(nodeData.Name, nodeData.Type,/* nodeData.DialogueType,*/ nodeData.Position, false);

                node.ID = nodeData.ID;
                node.Choices = choices;
                node.Text = nodeData.Text;

                node.Draw();

                this.AddElement(node);
                OnAddNewDSNode?.Invoke(node);

                loadedNodes.Add(node.ID, node);

                if (string.IsNullOrEmpty(nodeData.GroupID))
                {
                    continue;
                }

                DSGroup group = loadedGroups[nodeData.GroupID];

                node.Group = group;

                group.AddElement(node);
            }
        }

        private void LoadNodesConnections(Dictionary<string, DSNode> loadedNodes)
        {
            foreach (KeyValuePair<string, DSNode> loadedNode in loadedNodes)
            {
                foreach (Port choicePort in loadedNode.Value.outputContainer.Children())
                {
                    DSChoiceSaveData choiceData = (DSChoiceSaveData)choicePort.userData;

                    if (string.IsNullOrEmpty(choiceData.NodeID))
                    {
                        continue;
                    }

                    DSNode nextNode = loadedNodes[choiceData.NodeID];

                    Port nextNodeInputPort = (Port)nextNode.inputContainer.Children().First();

                    Edge edge = choicePort.ConnectTo(nextNodeInputPort);

                    this.AddElement(edge);

                    loadedNode.Value.RefreshPorts();
                }
            }
        }
    }
}
