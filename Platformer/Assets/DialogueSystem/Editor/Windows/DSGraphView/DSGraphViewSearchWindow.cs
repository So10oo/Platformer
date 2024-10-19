using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace DialogueSystem.Editor
{
    public partial class DSGraphView
    {
        private void AddSearchWindow()
        {
            var searchWindow = ScriptableObject.CreateInstance<DSSearchWindow>();
            searchWindow.OnSelectedDSNode += OnSelectedDSNode;
            searchWindow.OnSelectedGroup += OnSelectedGroup;
            nodeCreationRequest = context => SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), searchWindow);
        }

        private void OnSelectedGroup(Vector2 position)
        {
            DSGroup group = CreateGroup("DialogueGroup", new Rect(GetLocalMousePosition(position), Vector2.zero));
            AddElement(group);
            OnAddNewGroup?.Invoke(group);
            foreach (GraphElement selectedElement in selection)
                if (selectedElement is DSNode node)
                    group.AddElement(node);
        }

        private void OnSelectedDSNode(Type type, Vector2 position)
        {
            DSNode node = CreateNode("DialogueName", type, GetLocalMousePosition(position));
            AddElement(node);
            OnAddNewDSNode?.Invoke(node);
        }

    }
}
