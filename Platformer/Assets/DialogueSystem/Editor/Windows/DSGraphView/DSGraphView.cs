using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace DialogueSystem.Editor
{
    public partial class DSGraphView : GraphView
    {
        private readonly DSEditorWindow editorWindow;
        private MiniMap _miniMap;

        public event Action<DSNode> OnAddNewDSNode;
        public event Action<DSGroup> OnAddNewGroup;
        public event Action<DSNode> OnCreateDSNode;
        public event Action<DSGroup> OnCreateDSGroup;

        public DSGraphView(DSEditorWindow dsEditorWindow)
        {
            editorWindow = dsEditorWindow;

            AddManipulators();
            AddGridBackground();
            AddSearchWindow();
            AddMiniMap();
            AddBlackboard();

            OnGroupElementsAdded();
            OnElementsDeleted();
            OnGroupElementRemoved();
            OnGraphViewChanged();

            OnAddNewDSNode += AddUngroupedNodeFromDictionary;//подписываемся на событие добавления ноды через SearchWindow(а есть еще варианты?нет...)
            OnAddNewGroup += AddGroupFromDictionary;//подписываемся на событие добавления групп через SearchWindow
            OnCreateDSNode += SubscriberRenameNode;
            OnCreateDSGroup += SubscriberRenameGroup;

            AddStyles();
            AddMiniMapStyles();
        }

        ~DSGraphView()
        {
            OnAddNewDSNode -= AddUngroupedNodeFromDictionary;
            OnAddNewGroup -= AddGroupFromDictionary;
            OnCreateDSNode -= SubscriberRenameNode;
            OnCreateDSGroup -= SubscriberRenameGroup;
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            var compatiblePorts = new List<Port>();
            ports.ForEach(port =>
            {
                if (startPort.node == port.node || startPort.direction == port.direction)
                    return;
                compatiblePorts.Add(port);
            });
            return compatiblePorts;
        }

        private void AddManipulators()
        {
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
        }

        #region CreateMetod
        private DSGroup CreateGroup(string title, Rect position)
        {
            var group = new DSGroup(title, position);
            OnCreateDSGroup?.Invoke(group);
            return group;
        }

        private DSNode CreateNode(string nodeName, Type nodeType, Vector2 position)
        {
            DSNode node = (DSNode)Activator.CreateInstance(nodeType);
            node.Initialize(nodeName, position, characters);
            node.Draw();
            node.OnDisconnectPorts += DeleteElements;
            OnCreateDSNode?.Invoke(node);
            return node;
        }

        private DSNode CreateNode(DSNodeSaveData data, List<CharacterField> characterFields)
        {
            DSNode node = (DSNode)Activator.CreateInstance(data.Type);
            node.Initialize(data, characterFields);
            node.Draw();
            node.OnDisconnectPorts += DeleteElements;
            OnCreateDSNode?.Invoke(node);
            return node;
        }

        #endregion

        #region Callbacks GraphView
        private void OnElementsDeleted()
        {
            deleteSelection = (operationName, askUser) =>
            {
                for (int i = selection.Count - 1; i >= 0;)
                {
                    var oldSelectionCount = selection.Count;
                    var select = selection[i];
                    switch (select)
                    {
                        case DSNode node:
                            OnNodeDeleted(node);
                            node.OnDisconnectPorts -= DeleteElements;
                            goto Deleted;
                        case DSGroup group:
                            OnGroupDeleted(group);
                            goto Deleted;
                        case Edge edge:
                            goto Deleted;
                        case BlackboardField blackboardField:
                            var characterField = (CharacterField)blackboardField.userData;
                            _blackboard.Remove(characterField);
                            characters.Remove(characterField);
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
            graphViewChanged = (changes) =>
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

        #region MiniMap
        private void AddMiniMap()
        {
            _miniMap = new MiniMap()
            {
                anchored = true,
            };
            //miniMap.style.unityTextOverflowPosition = PositionType.
            _miniMap.SetPosition(new Rect(20, 50, 200, 180));
            Add(_miniMap);
            _miniMap.visible = false;
        }

        private void AddMiniMapStyles()
        {
            StyleColor backgroundColor = new(new Color32(29, 29, 30, 255));
            StyleColor borderColor = new(new Color32(51, 51, 51, 255));

            _miniMap.style.backgroundColor = backgroundColor;
            _miniMap.style.borderTopColor = borderColor;
            _miniMap.style.borderRightColor = borderColor;
            _miniMap.style.borderBottomColor = borderColor;
            _miniMap.style.borderLeftColor = borderColor;
        }

        public void ToggleMiniMap() => _miniMap.visible = !_miniMap.visible;

        #endregion

        private void AddStyles()
        {
            this.AddStyleSheets(
                PathToStyle.DSGraphViewStyles,
                PathToStyle.DSNodeStyles
            );
        }

        public Vector2 GetLocalMousePosition(Vector2 mousePosition, bool isSearchWindow = false)
        {
            Vector2 worldMousePosition = mousePosition;
            if (isSearchWindow)
                worldMousePosition = editorWindow.rootVisualElement.ChangeCoordinatesTo(editorWindow.rootVisualElement.parent, mousePosition - editorWindow.position.position);
            return contentViewContainer.WorldToLocal(worldMousePosition);
        }

        public void ClearGraph()
        {
            graphElements.ForEach(graphElement => RemoveElement(graphElement));
            groups.Clear();
            groupedNodes.Clear();
            ungroupedNodes.Clear();
            globalCounterErrors.ResetErrors();
            ClearCharacters();
        }

        private void AddGridBackground()
        {
            var gridBackground = new GridBackground();
            gridBackground.StretchToParentSize();
            Insert(0, gridBackground);
        }
    }
}