using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace DialogueSystem.Editor
{
    public class DSMiniMap : MiniMap
    {
        public DSMiniMap() 
        {
            anchored = true;
            visible = false;
            SetPosition(new Rect(20, 50, 200, 180));
            AddStyles();
        }

        private void AddStyles()
        {
            StyleColor backgroundColor = new(new Color32(29, 29, 30, 255));
            StyleColor borderColor = new(new Color32(51, 51, 51, 255));

            style.backgroundColor = backgroundColor;
            style.borderTopColor = borderColor;
            style.borderRightColor = borderColor;
            style.borderBottomColor = borderColor;
            style.borderLeftColor = borderColor;
        }

        public void Toggle() => visible = !visible;

    }
}
