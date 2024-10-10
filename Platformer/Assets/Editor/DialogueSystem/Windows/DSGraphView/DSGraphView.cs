using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace DS.Windows
{
    using Elements;
    using Utilities;

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

            OnAddNewDSNode += AddUngroupedNodeFromDictionary;//������������� �� ������� ���������� ���� ����� SearchWindow(� ���� ��� ��������?���...)
            OnAddNewGroup += AddGroupFromDictionary;//������������� �� ������� ���������� ����� ����� SearchWindow
            OnCreateDSNode += OnNodeCreate;
            OnCreateDSGroup += OnGroupCreate;

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
        private DSGroup CreateGroup(string title, Vector2 position)
        {
            DSGroup group = new DSGroup(title, position);
            OnCreateDSGroup?.Invoke(group);
            return group;
        }

        private DSNode CreateNode(string nodeName, Type nodeType, Vector2 position)
        {
            DSNode node = (DSNode)Activator.CreateInstance(nodeType);
            node.Initialize(nodeName, this, position);
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
                for (int i = selection.Count - 1; i >= 0; i--)
                {
                    switch (selection[i])
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
                            if (selection[i] is GraphElement graphElement)
                                RemoveElement(graphElement);
                            break;
                        default:
                            Debug.Log(selection[i]);
                            break;
                    }
                }
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
            DSGroup group = CreateGroup("DialogueGroup", GetLocalMousePosition(position, true));
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
                "DialogueSystem/DSGraphViewStyles.uss",
                "DialogueSystem/DSNodeStyles.uss"
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
            //groups.Clear();
            //groupedNodes.Clear();
            //ungroupedNodes.Clear();
        }
        private void AddGridBackground()
        {
            GridBackground gridBackground = new GridBackground();

            gridBackground.StretchToParentSize();

            Insert(0, gridBackground);
        }

    }
}