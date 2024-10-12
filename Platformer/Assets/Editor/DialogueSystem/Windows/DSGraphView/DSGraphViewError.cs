using Assets.Editor.DialogueSystem.Interface;
using DS.Data.Error;
using DS.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


namespace DS.Windows
{
    public partial class DSGraphView
    {
        private Dictionary<string, DSErrorData<DSNode>> ungroupedNodes = new();
        private Dictionary<string, DSErrorData<DSGroup>> groups = new();
        private Dictionary<DSGroup, Dictionary<string, DSErrorData<DSNode>>> groupedNodes = new();

        //private int _countErrors = 0;
        //public event Action<bool> ErrorsStateChanged;
        //private int CountErrors
        //{
        //    set
        //    {
        //        _countErrors = value;
        //        ErrorsStateChanged?.Invoke(_countErrors != 0);
        //    }
        //    get
        //    {
        //        return _countErrors;
        //    }
        //}
        public GlobalCounterErrors globalCounterErrors = new ();

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
            string nodeName = node.DialogueName.ToLower();
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
            string nodeName = node.DialogueName.ToLower();
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
        private void AddElementFromDictionary<T>(Dictionary<string, DSErrorData<T>> dictionary, T element , string name) where T : GraphElement, ISetStyleError
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
        private void OnGroupElementsAdded()
        {
            elementsAddedToGroup = (group, elements) =>
            {
                foreach (var element in elements)
                {
                    if (element is DSNode node)
                    {
                        RemoveUngroupedNodeFromDictionary(node);
                        node.Group = (DSGroup)group;
                        AddGroupedNodeFromDictionary(node, (DSGroup)group);
                    }
                }
            };
        }
        private void OnGroupElementRemoved()
        {
            elementsRemovedFromGroup = (group, elements) =>
            {
                foreach (var element in elements)
                {
                    if (element is DSNode node)
                    {
                        RemoveGroupedNodeFromDictionary(node, (DSGroup)group);
                        node.Group = null;
                        AddUngroupedNodeFromDictionary(node);
                    }
                }
            };
        }
        #endregion

        #region StartEndCallbaksErrorSystem
        private void OnGroupDeleted(DSGroup group)
        {
            group.OnRename -= OnRenameGroup;
            RemoveGroupFromDictionary(group);
            var listElementsForGroup = group.containedElements.ToList();
            for (int j = listElementsForGroup.Count - 1; j >= 0; j--)
            {
                var element = listElementsForGroup[j];
                if (element is DSNode node)
                    group.RemoveElement(node);
                else
                    Debug.Log("Error element is not DSNode");
            }
        }
        private void OnNodeDeleted(DSNode node)
        {
            node.OnRename -= OnRenameNode;

            if (node.Group != null)
                node.Group.RemoveElement(node);
            RemoveUngroupedNodeFromDictionary(node);
            node.DisconnectAllPorts();
        }
        private void OnGroupCreate(DSGroup group)
        {
            group.OnRename += OnRenameGroup;
        }
        private void OnNodeCreate(DSNode node)
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
                RemoveGroupedNodeFromDictionary(node, node.Group,oldValue);
                AddGroupedNodeFromDictionary(node, node.Group);
            }
                 
        }
        #endregion
    }
}
