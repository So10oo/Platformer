using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace DialogueSystem.Editor
{
    public abstract class DSGraphViewBase : GraphView
    {
        DSEditorWindow _editorWindow;
        protected DSMiniMap miniMap;
        protected DSCharacterBlackboard characterBlackboard;
        public List<CharacterField> characters => characterBlackboard.characters;

        public DSGraphViewBase(DSEditorWindow dsEditorWindow)
        {
            _editorWindow = dsEditorWindow;
            SetResetCallbacks();
            AddGridBackground();
            AddMiniMap();
            AddCharacterBlackboard();
            AddManipulators();
            AddStyles();
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

        private void AddGridBackground()
        {
            var gridBackground = new GridBackground();
            gridBackground.StretchToParentSize();
            Insert(0, gridBackground);
        }

        private void AddStyles()
        {
            this.AddStyleSheets(
                PathToStyle.DSGraphViewStyles,
                PathToStyle.DSNodeStyles
            );
        }

        public Vector2 GetLocalMousePosition(Vector2 mousePosition)
        {
            Vector2 worldMousePosition = mousePosition;
            worldMousePosition = _editorWindow.rootVisualElement.ChangeCoordinatesTo(_editorWindow.rootVisualElement.parent, mousePosition - _editorWindow.position.position);
            return contentViewContainer.WorldToLocal(worldMousePosition);
        }

        private void AddCharacterBlackboard()
        {
            characterBlackboard = new DSCharacterBlackboard();
            Add(characterBlackboard);
        }

        private void AddMiniMap()
        {
            miniMap = new DSMiniMap();
            Add(miniMap);
        }

        public void ToggleMiniMap() => miniMap.Toggle();

        private void SetResetCallbacks()
        {
            graphViewChanged = null;
            deleteSelection = null;
            elementsAddedToGroup = null;
            elementsRemovedFromGroup = null;
        }

        protected void ClearElements() => graphElements.ForEach(graphElement => RemoveElement(graphElement));

        public void AddCharacterField(CharacterField characterField) => characterBlackboard.AddCharacterField(characterField);
        
    }
}
