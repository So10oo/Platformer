using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace DialogueSystem.Editor
{
    public partial class DSGraphView
    {
        int _count = 0;

        private void AddSearchWindow()
        {
            var searchWindow = ScriptableObject.CreateInstance<DSSearchWindow>();
            searchWindow.OnSelectedDSNode += OnSelectedDSNode;
            searchWindow.OnSelectedGroup += OnSelectedGroup;
            nodeCreationRequest = context => SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), searchWindow);
        }

        private void OnSelectedGroup(Vector2 position)
        {
            DSGroup group = CreateGroup($"DialogueGroup{_count++}", new Rect(GetLocalMousePosition(position), Vector2.zero));
            AddGroup(group);
            foreach (GraphElement selectedElement in selection)
                if (selectedElement is DSNode node)
                    group.AddElement(node);
        }

        private void OnSelectedDSNode(Type type, Vector2 position)
        {
            DSNode node = CreateNode($"DialogueName{_count++}", type, GetLocalMousePosition(position));
            AddNode(node);
        }

    }
}
