using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace DS.Windows
{
    using Elements;
    using System;

    public class DSSearchWindow : ScriptableObject, ISearchWindowProvider
    {
        private Texture2D indentationIcon;
        public event Action<Type, Vector2> OnSelectedDSNode;
        public event Action<Vector2> OnSelectedGroup;

        private void OnEnable()
        {
            if (indentationIcon == null)
            {
                indentationIcon = new Texture2D(1, 1);
                indentationIcon.SetPixel(0, 0, Color.clear);
                indentationIcon.Apply();
            }
        }

        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            List<SearchTreeEntry> searchTreeEntries = new List<SearchTreeEntry>()
            {
                new SearchTreeGroupEntry(new GUIContent("Create Elements")),
                new SearchTreeGroupEntry(new GUIContent("Dialogue Nodes"), 1),
                new SearchTreeEntry(new GUIContent("Single Choice", indentationIcon))
                {
                    userData = typeof(DSSingleChoiceNode),
                    level = 2
                },
                new SearchTreeEntry(new GUIContent("Multiple Choice", indentationIcon))
                {
                    userData = typeof(DSMultipleChoiceNode),
                    level = 2
                },
                new SearchTreeGroupEntry(new GUIContent("Dialogue Groups"), 1),
                new SearchTreeEntry(new GUIContent("Single Group", indentationIcon))
                {
                    userData = typeof(DSGroup),
                    level = 2
                }
            };
            return searchTreeEntries;
        }

        public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
        {
            Vector2 screenMousePosition = context.screenMousePosition;
            switch (SearchTreeEntry.userData)
            {
                case Type type when type.IsSubclassOf(typeof(DSNode)):
                    {
                        OnSelectedDSNode?.Invoke(type, screenMousePosition);
                        return true;
                    }
                case Type type when type == typeof(DSGroup):
                    {
                        OnSelectedGroup?.Invoke(screenMousePosition);
                        return true;
                    }
                default:
                    {
                        Debug.Log($"Error {SearchTreeEntry.userData}");
                        return false;
                    }
            }
        }
    }
}