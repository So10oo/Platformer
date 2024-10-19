using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace DialogueSystem.Editor
{
    public partial class DSGraphView : DSGraphViewBase
    {
        public ErrorController errorController;

        public event Action<DSNode> OnAddNode;
        public event Action<DSGroup> OnAddGroup;
        public event Action<DSNode> OnRemovedNode;
        public event Action<DSGroup> OnRemovedGroup;

        public DSGraphView(DSEditorWindow dsEditorWindow) : base(dsEditorWindow)
        {
            AddSearchWindow();
            OnElementsDeleted();
            OnGraphViewChanged();

            errorController = new ErrorController();
            errorController.Subscribe(this);
        }

        ~DSGraphView()
        {
            errorController.Unsubscribe(this);
        }

        #region CreateMetod
        private DSGroup CreateGroup(string title, Rect position, string id = null)
        {
            var group = new DSGroup(title, position, id);
            return group;
        }

        private DSNode CreateNode(string nodeName, Type nodeType, Vector2 position)
        {
            DSNode node = (DSNode)Activator.CreateInstance(nodeType);
            node.Initialize(nodeName, position, characterBlackboard.characters);
            node.Draw();
            node.OnDisconnectPorts += DeleteElements;
            return node;
        }

        private DSNode CreateNode(DSNodeSaveData data, List<CharacterField> characterFields)
        {
            DSNode node = (DSNode)Activator.CreateInstance(data.Type);
            node.Initialize(data, characterFields);
            node.Draw();
            node.OnDisconnectPorts += DeleteElements;
            return node;
        }
        #endregion

        #region LoadedMetod
        public DSNode LoadNode(DSNodeSaveData data, List<CharacterField> characterFields)
        {
            var node = CreateNode(data, characterFields);
            AddNode(node);
            return node;
        }
        public DSGroup LoadGroup(string title, Rect position, string id = null)
        {
            var group = CreateGroup(title, position, id);
            AddGroup(group);
            return group;
        }
        #endregion

        #region AddedMetod
        private void AddNode(DSNode node)
        {
            AddElement(node);
            OnAddNode?.Invoke(node);
        }

        private void AddGroup(DSGroup group)
        {
            AddElement(group);
            OnAddGroup?.Invoke(group);
        }
        #endregion

        #region Callbacks GraphView
        private void OnElementsDeleted()
        {
            deleteSelection += (operationName, askUser) =>
            {
                for (int i = selection.Count - 1; i >= 0;)
                {
                    var oldSelectionCount = selection.Count;
                    var select = selection[i];
                    switch (select)
                    {
                        case DSNode node:
                            OnRemovedNode?.Invoke(node);
                            node.OnDisconnectPorts -= DeleteElements;
                            goto Deleted;
                        case DSGroup group:
                            OnRemovedGroup?.Invoke(group);
                            goto Deleted;
                        case Edge edge:
                            goto Deleted;
                        case BlackboardField blackboardField:
                            var characterField = (CharacterField)blackboardField.userData;
                            characterBlackboard.Remove(characterField);
                            break;
                        Deleted:
                            if (select is GraphElement graphElement)
                                RemoveElement(graphElement);
                            break;
                        default:
                            Debug.Log(select);
                            break;
                    }
                    var deleteElements = oldSelectionCount - selection.Count;
                    var subtractI = Mathf.Max(1, deleteElements);
                    i -= subtractI;
                }

            };
        }

        private void OnGraphViewChanged()
        {
            graphViewChanged += (changes) =>
            {
                if (changes.edgesToCreate != null)
                {
                    foreach (var edge in changes.edgesToCreate)
                    {
                        var nextNode = (DSNode)edge.input.node;
                        var choiceData = (DSChoiceSaveData)edge.output.userData;
                        choiceData.NodeID = nextNode.ID;
                    }
                    if (changes.elementsToRemove != null)
                    {
                        foreach (GraphElement element in changes.elementsToRemove)
                        {
                            if (element is Edge edge)
                            {
                                var choiceData = (DSChoiceSaveData)edge.output.userData;
                                choiceData.NodeID = string.Empty;
                            }
                        }
                    }
                }
                return changes;
            };
        }
        #endregion


        public void ClearGraph()
        {
            this.ClearElements();
            errorController.ClearData();
            characterBlackboard.ClearCharacters();
        }
    }
}