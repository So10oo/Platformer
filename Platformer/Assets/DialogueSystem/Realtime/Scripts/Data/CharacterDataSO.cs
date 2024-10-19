using DialogueSystem.Editor;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DialogueSystem.Realtime
{
    public class CharacterDataSO : ScriptableObject
    {
        [field: SerializeField, ReadOnly]
        public string Name { get; private set; }

        [field: SerializeField, ReadOnly]
        public Texture2D Icon { get; private set; }

        [field: SerializeField, ReadOnly]
        public string ID { get; private set; }

#if UNITY_EDITOR
        public static CharacterDataSO CreateInstance(CharacterField characterField)
        {
            CharacterDataSO instance = ScriptableObject.CreateInstance<CharacterDataSO>();
            instance.name = characterField.Name;
            instance.Name = characterField.Name;
            instance.Icon = characterField.Icon;
            instance.ID = characterField.ID;    
            return instance;
        }
#endif
    }
}