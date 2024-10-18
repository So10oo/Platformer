using Sirenix.OdinInspector;
using UnityEngine;

namespace DialogueSystem.Editor
{
    public class CharacterSaveData 
    {
        [field: SerializeField, ReadOnly]
        public string Name { get; set; }

        [field: SerializeField, ReadOnly]
        public Texture2D Icon { get; set; }

        [field: SerializeField, ReadOnly]
        public string ID { get; set; }

        public CharacterSaveData(CharacterField characterField)
        {
            Name = characterField.Name;
            Icon = characterField.Icon; 
            ID = characterField.ID;
        }
    }
}
