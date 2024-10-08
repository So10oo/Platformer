using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace DS.Windows
{
    using Elements;
    //using Enumerations;
    using System;

    public class DSSearchWindow : ScriptableObject, ISearchWindowProvider
    {
        private DSGraphView graphView;
        private Texture2D indentationIcon;

        public void Initialize(DSGraphView dsGraphView)
        {
            graphView = dsGraphView;

            indentationIcon = new Texture2D(1, 1);
            indentationIcon.SetPixel(0, 0, Color.clear);
            indentationIcon.Apply();
        }

        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            List<SearchTreeEntry> searchTreeEntries = new List<SearchTreeEntry>()
            {
                new SearchTreeGroupEntry(new GUIContent("Create Elements")),
                new SearchTreeGroupEntry(new GUIContent("Dialogue Nodes"), 1),
                new SearchTreeEntry(new GUIContent("Single Choice", indentationIcon))
                {
                    userData = typeof(DSSingleChoiceNode)/*DSDialogueType.SingleChoice*/,
                    level = 2
                },
                new SearchTreeEntry(new GUIContent("Multiple Choice", indentationIcon))
                {
                    userData = typeof(DSMultipleChoiceNode)/*DSDialogueType.MultipleChoice*/,
                    level = 2
                },
                new SearchTreeGroupEntry(new GUIContent("Dialogue Groups"), 1),
                new SearchTreeEntry(new GUIContent("Single Group", indentationIcon))
                {
                    userData = typeof(Group),
                    level = 2
                }
            };

            return searchTreeEntries;
        }

        public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
        {
            Vector2 localMousePosition = graphView.GetLocalMousePosition(context.screenMousePosition, true);

            switch ((Type)SearchTreeEntry.userData)
            {
                case Type type when type == typeof(DSSingleChoiceNode)/*DSDialogueType.SingleChoice*/:
                    {
                        DSSingleChoiceNode singleChoiceNode = (DSSingleChoiceNode)graphView.CreateNode("DialogueName", (Type)SearchTreeEntry.userData/*, DSDialogueType.SingleChoice*/, localMousePosition);

                        graphView.AddElement(singleChoiceNode);

                        return true;
                    }
                case Type type when type == typeof(DSMultipleChoiceNode)/*DSDialogueType.MultipleChoice*/:
                    {
                        DSMultipleChoiceNode multipleChoiceNode = (DSMultipleChoiceNode)graphView.CreateNode("DialogueName", (Type)SearchTreeEntry.userData/*, DSDialogueType.MultipleChoice*/, localMousePosition);

                        graphView.AddElement(multipleChoiceNode);

                        return true;
                    }

                case Type type when type == typeof(Group):
                    {
                        graphView.CreateGroup("DialogueGroup", localMousePosition);

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