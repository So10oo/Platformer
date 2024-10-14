using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace DialogueSystem.Editor
{
    public class DSGroup : Group, ISetStyleError
    {
        public string ID { get; set; }
        public event Action<DSGroup, string, string> OnRename;
        private Color defaultBorderColor;
        private float defaultBorderWidth;
        public DSGroup(string groupTitle, Rect rect)
        {
            ID = Guid.NewGuid().ToString();
            title = groupTitle;
            SetPosition(rect);
            defaultBorderColor = contentContainer.style.borderBottomColor.value;
            defaultBorderWidth = contentContainer.style.borderBottomWidth.value;
        }
        public void SetErrorStyle(Color color)
        {
            contentContainer.style.borderBottomColor = color;
            contentContainer.style.borderLeftColor = color;
            contentContainer.style.borderRightColor = color;
            contentContainer.style.borderTopColor = color;
            contentContainer.style.borderBottomWidth = 2f;
            contentContainer.style.borderLeftWidth = 2f;
            contentContainer.style.borderRightWidth = 2f;
            contentContainer.style.borderTopWidth = 2f;
        }
        public void SetDefaultStyle()
        {
            contentContainer.style.borderBottomColor = defaultBorderColor;
            contentContainer.style.borderLeftColor = defaultBorderColor;
            contentContainer.style.borderRightColor = defaultBorderColor;
            contentContainer.style.borderTopColor = defaultBorderColor;
            contentContainer.style.borderBottomWidth = defaultBorderWidth;
            contentContainer.style.borderLeftWidth = defaultBorderWidth;
            contentContainer.style.borderRightWidth = defaultBorderWidth;
            contentContainer.style.borderTopWidth = defaultBorderWidth;
        }
        protected override void OnGroupRenamed(string oldName, string newName)
        {
            OnRename?.Invoke(this, oldName, newName);
        }
    }
}