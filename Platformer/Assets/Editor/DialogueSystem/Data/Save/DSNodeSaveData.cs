using DS.Elements;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DS.Data.Save
{
    //using Enumerations;

    [Serializable]
    public class DSNodeSaveData
    {
        [field: SerializeField] public string ID { get; set; }
        [field: SerializeField] public string Name { get; set; }
        [field: SerializeField] public string Text { get; set; }
        [field: SerializeField] public List<DSChoiceSaveData> Choices { get; set; }
        [field: SerializeField] public string GroupID { get; set; }
        [field: SerializeField] public Type Type { get; set; }
        [field: SerializeField] public Vector2 Position { get; set; }

        public DSNodeSaveData()
        {

        }

        public DSNodeSaveData(DSNode node)
        {
            ID = node.ID;
            Name = node.DialogueName;
            Choices = node.Choices.Clone();
            Text = node.Text;
            GroupID = node.Group?.ID;
            Type = node.GetType();
            Position = node.GetPosition().position;
        }
    }
}