using DialogueSystem.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem.Editor
{
    [Serializable]
    public class DSNodeSaveData
    {
        [field: SerializeField] public string ID { get; set; }
        [field: SerializeField] public string Name { get; set; }
        [field: SerializeField] public string Text { get; set; }
        [field: SerializeField] public List<DSChoiceSaveData> Choices { get; set; }
        [field: SerializeField] public string GroupID { get; set; }
        [field: SerializeField] public string StringType { private get; set; }
        [field: SerializeField] public Vector2 Position { get; set; }
        [field: SerializeField] public string CharacterID { get; set; }

        public Type Type => System.Type.GetType(StringType);

        public DSNodeSaveData(DSNode node)
        {
            ID = node.ID;
            Name = node.DialogueName;
            Choices = node.Choices.Clone();
            Text = node.Text;
            GroupID = node.Group?.ID;
            StringType = node.GetType().ToString();
            Position = node.GetPosition().position;
            CharacterID = node.Character?.ID;
        }
    }
}