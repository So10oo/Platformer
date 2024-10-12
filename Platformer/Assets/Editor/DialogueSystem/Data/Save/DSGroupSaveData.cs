using DS.Elements;
using System;
using UnityEngine;

namespace DS.Data.Save
{
    [Serializable]
    public class DSGroupSaveData
    {
        [field: SerializeField] public string ID { get; set; }
        [field: SerializeField] public string Name { get; set; }
        [field: SerializeField] public Vector2 Position { get; set; }

        public DSGroupSaveData() { }

        public DSGroupSaveData(DSGroup group)
        {
            ID = group.ID;
            Name = group.title;
            Position = group.GetPosition().position;
        }
    }
}