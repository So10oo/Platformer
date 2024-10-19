using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;

namespace DialogueSystem.Editor
{
    public class ErrorController
    {
        private Dictionary<string, DSErrorData<DSNode>> ungroupedNodes;
        private Dictionary<string, DSErrorData<DSGroup>> groups;
        private Dictionary<DSGroup, Dictionary<string, DSErrorData<DSNode>>> groupedNodes;

        public readonly GlobalCounterErrors globalCounterErrors = new();

        public ErrorController()
        {
            ungroupedNodes = new();
            groups = new();
            groupedNodes = new();
        }

        public void Subscribe(DSGraphView graph)
        {
            graph.elementsAddedToGroup += OnGroupElementsAdded;
            graph.elementsRemovedFromGroup += OnGroupElementRemoved;
            graph.OnAddNode += OnAddedNode;
            graph.OnAddGroup += OnAddedGroup;
            graph.OnRemovedNode += OnRemovedNode;
            graph.OnRemovedGroup += OnRemovedGroup;
        }

        public void Unsubscribe(DSGraphView graph)
        {
            graph.elementsAddedToGroup -= OnGroupElementsAdded;
            graph.elementsRemovedFromGroup -= OnGroupElementRemoved;
            graph.OnAddNode -= OnAddedNode;
            graph.OnAddGroup -= OnAddedGroup;
            graph.OnRemovedNode -= OnRemovedNode;
            graph.OnRemovedGroup -= OnRemovedGroup;
        }

        #region Dictionary controls
        private void AddUngroupedNodeFromDictionary(DSNode node)
        {
            AddElementFromDictionary(ungroupedNodes, node, node.DialogueName);
        }
        private void RemoveUngroupedNodeFromDictionary(DSNode node)
        {
            RemoveElementFromDictionary(ungroupedNodes, node, node.DialogueName);
        }
        private void AddGroupedNodeFromDictionary(DSNode node, DSGroup group)
        {
            string nodeName = node.DialogueName;
            AddGroupedNodeFromDictionary(node, group, nodeName);
        }
        private void AddGroupedNodeFromDictionary(DSNode node, DSGroup group, string nodeName)
        {
            nodeName = nodeName.ToLower();
            if (!groupedNodes.ContainsKey(group))
                groupedNodes.Add(group, new Dictionary<string, DSErrorData<DSNode>>());
            AddElementFromDictionary(groupedNodes[group], node, nodeName);
        }
        private void RemoveGroupedNodeFromDictionary(DSNode node, DSGroup group)
        {
            string nodeName = node.DialogueName;
            RemoveGroupedNodeFromDictionary(node, group, nodeName);
        }
        private void RemoveGroupedNodeFromDictionary(DSNode node, DSGroup group, string nodeName)
        {
            nodeName = nodeName.ToLower();
            groupedNodes[group][nodeName].Remove(node);
            if (groupedNodes[group][nodeName].isEmpty)
            {
                groupedNodes[group].Remove(nodeName);
                if (groupedNodes[group].Count == 0)
                    groupedNodes.Remove(group);
            }
        }
        private void AddGroupFromDictionary(DSGroup group)
        {
            AddGroupFromDictionary(group, group.title);
        }
        private void AddGroupFromDictionary(DSGroup group, string currentNameInDictionary)
        {
            AddElementFromDictionary(groups, group, currentNameInDictionary);
        }
        private void RemoveGroupFromDictionary(DSGroup group)
        {
            RemoveGroupFromDictionary(group, group.title);
        }
        private void RemoveGroupFromDictionary(DSGroup group, string currentNameInDictionary)
        {
            RemoveElementFromDictionary(groups, group, currentNameInDictionary);
        }
        private void OnRenameGroup(DSGroup group, string oldName, string newName)
        {
            RemoveGroupFromDictionary(group, oldName);
            newName = newName.RemoveWhitespaces().RemoveSpecialCharacters();
            group.title = newName;
            AddGroupFromDictionary(group, newName);
        }
        private void AddElementFromDictionary<T>(Dictionary<string, DSErrorData<T>> dictionary, T element, string name) where T : GraphElement, ISetStyleError
        {
            name = name.ToLower();
            if (!dictionary.ContainsKey(name))
            {
                var errorData = new DSErrorData<T>(globalCounterErrors);
                errorData.Add(element);
                dictionary.Add(name, errorData);
            }
            else
            {
                dictionary[name].Add(element);
            }
        }
        private void RemoveElementFromDictionary<T>(Dictionary<string, DSErrorData<T>> dictionary, T element, string name) where T : GraphElement, ISetStyleError
        {
            name = name.ToLower();
            dictionary[name].Remove(element);
            if (dictionary[name].isEmpty)
                dictionary.Remove(name);
        }
        #endregion

        #region Callbacks
        private void OnGroupElementsAdded(Group group, IEnumerable<GraphElement> elements)
        {
            //elementsAddedToGroup = (group, elements) =>
            //{
                foreach (var element in elements)
                {
                    if (element is DSNode node)
                    {
                        RemoveUngroupedNodeFromDictionary(node);
                        node.Group = (DSGroup)group;
                        AddGroupedNodeFromDictionary(node, (DSGroup)group);
                    }
                }
            //};
        }
        private void OnGroupElementRemoved(Group group, IEnumerable<GraphElement> elements)
        {
            //elementsRemovedFromGroup = (group, elements) =>
            //{
                foreach (var element in elements)
                {
                    if (element is DSNode node)
                    {
                        RemoveGroupedNodeFromDictionary(node, (DSGroup)group);
                        node.Group = null;
                        AddUngroupedNodeFromDictionary(node);
                    }
                }
            //};
        }
        private void OnAddedNode(DSNode node)
        {
            AddUngroupedNodeFromDictionary(node);
            SubscriberRenameNode(node);
        }
        private void OnAddedGroup(DSGroup group)
        {
            AddGroupFromDictionary(group);
            SubscriberRenameGroup(group);
        }
        private void OnRemovedGroup(DSGroup group)
        {
            group.OnRename -= OnRenameGroup;
            RemoveGroupFromDictionary(group);
            var listElementsForGroup = group.containedElements.ToList();
            for (int j = listElementsForGroup.Count - 1; j >= 0; j--)
            {
                var element = listElementsForGroup[j];
                if (element is DSNode node)
                    group.RemoveElement(node);
            }
        }
        private void OnRemovedNode(DSNode node)
        {
            node.OnRename -= OnRenameNode;

            if (node.Group != null)
                node.Group.RemoveElement(node);
            RemoveUngroupedNodeFromDictionary(node);
            node.DisconnectAllPorts();
        }
        #endregion

        #region StartEndCallbaksErrorSystem
        private void SubscriberRenameGroup(DSGroup group)
        {
            group.OnRename += OnRenameGroup;
        }
        private void SubscriberRenameNode(DSNode node)
        {
            node.OnRename += OnRenameNode;
        }
        private void OnRenameNode(DSNode node, string oldValue, string newValue)
        {
            if (node.Group == null)
            {
                RemoveElementFromDictionary(ungroupedNodes, node, oldValue);
                AddElementFromDictionary(ungroupedNodes, node, newValue);
            }
            else
            {
                RemoveGroupedNodeFromDictionary(node, node.Group, oldValue);
                AddGroupedNodeFromDictionary(node, node.Group, newValue);
            }

        }
        #endregion

        public void ClearData()
        {
            ungroupedNodes.Clear();
            groups.Clear();
            groupedNodes.Clear();
            globalCounterErrors.ResetErrors();
        }
    }
}
