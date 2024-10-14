using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace DialogueSystem.Editor
{
    public partial class DSGraphView : GraphView
    {
        private DSEditorWindow editorWindow;

        private MiniMap miniMap;

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
            DSGroup group = new DSGroup(title, position);
            OnCreateDSGroup?.Invoke(group);
            return group;
        }

        private DSNode CreateNode(string nodeName, Type nodeType, Vector2 position,bool showDraw = true)
        {
            DSNode node = (DSNode)Activator.CreateInstance(nodeType);
            node.Initialize(nodeName, this, position);

            if (showDraw)
            {
                node.Draw();
            }
            
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
                            //RemoveElement(node);
                            goto Deleted;
                        case DSGroup group:
                            OnGroupDeleted(group);
                            goto Deleted;
                        //RemoveElement(group);
                        //break;
                        case Edge edge:
                            goto Deleted;
                        //RemoveElement(edge);
                        //break;
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
                    i-= subtractI;
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
                        DSNode nextNode = (DSNode)edge.input.node;
                        var choiceData = (DSChoiceSaveData)edge.output.userData;
                        choiceData.NodeID = nextNode.ID;
                    }
                    if (changes.elementsToRemove!=null)
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

        #region SearchWindow
        private void AddSearchWindow()
        {
            var searchWindow = ScriptableObject.CreateInstance<DSSearchWindow>();
            searchWindow.OnSelectedDSNode += SearchWindow_OnSelectedDSNode;
            searchWindow.OnSelectedGroup += SearchWindow_OnSelectedGroup;
            nodeCreationRequest = context => SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), searchWindow);
        }

        private void SearchWindow_OnSelectedGroup(Vector2 position)
        {
            DSGroup group = CreateGroup("DialogueGroup", new Rect(GetLocalMousePosition(position, true),Vector2.zero));
            AddElement(group);
            OnAddNewGroup?.Invoke(group);
            foreach (GraphElement selectedElement in selection)
                if (selectedElement is DSNode node)
                    group.AddElement(node);
        }

        private void SearchWindow_OnSelectedDSNode(Type type, Vector2 position)
        {
            DSNode node = CreateNode("DialogueName", type, GetLocalMousePosition(position, true));
            AddElement(node);
            OnAddNewDSNode?.Invoke(node);
        }
        #endregion


        #region MiniMap
        private void AddMiniMap()
        {
            miniMap = new MiniMap()
            {
                anchored = true
            };

            miniMap.SetPosition(new Rect(15, 50, 200, 180));

            Add(miniMap);

            miniMap.visible = false;
        }
        private void AddMiniMapStyles()
        {
            StyleColor backgroundColor = new StyleColor(new Color32(29, 29, 30, 255));
            StyleColor borderColor = new StyleColor(new Color32(51, 51, 51, 255));

            miniMap.style.backgroundColor = backgroundColor;
            miniMap.style.borderTopColor = borderColor;
            miniMap.style.borderRightColor = borderColor;
            miniMap.style.borderBottomColor = borderColor;
            miniMap.style.borderLeftColor = borderColor;
        }
        public void ToggleMiniMap()
        {
            miniMap.visible = !miniMap.visible;
        }
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
            {
                worldMousePosition = editorWindow.rootVisualElement.ChangeCoordinatesTo(editorWindow.rootVisualElement.parent, mousePosition - editorWindow.position.position);

            }

            Vector2 localMousePosition = contentViewContainer.WorldToLocal(worldMousePosition);

            return localMousePosition;
        }

        public void ClearGraph() 
        { 
            graphElements.ForEach(graphElement => RemoveElement(graphElement));
            groups.Clear();
            groupedNodes.Clear();
            ungroupedNodes.Clear();
            globalCounterErrors.ResetErrors();
        }
        private void AddGridBackground()
        {
            GridBackground gridBackground = new GridBackground();

            gridBackground.StretchToParentSize();

            Insert(0, gridBackground);
        }

    }
}