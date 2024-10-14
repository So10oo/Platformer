using System;
using UnityEngine;

namespace DialogueSystem.Editor
{
    [Serializable]
    public class DSChoiceSaveData : ICloneable
    {
        [field: SerializeField] public string Text { get; set; }
        [field: SerializeField] public string NodeID { get; set; }
        public object Clone() => new DSChoiceSaveData(Text, NodeID);
        public DSChoiceSaveData()
        {

        }
        public DSChoiceSaveData(string text, string nodeID)
        {
            Text = text;
            NodeID = nodeID;
        }
    }
}