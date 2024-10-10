using Assets.Editor.DialogueSystem.Interface;
using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace DS.Elements
{
    public class DSGroup : Group, ISetStyleError
    {
        public string ID { get;private set; }
        public event Action<DSGroup,string, string> OnRename;

        private Color defaultBorderColor;
        private float defaultBorderWidth;

        public DSGroup(string groupTitle, Vector2 position)
        {
            ID = Guid.NewGuid().ToString();
            title = groupTitle;
            SetPosition(new Rect(position, Vector2.zero));
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