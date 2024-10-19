using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace DialogueSystem.Editor
{
    public class DSCharacterBlackboard : Blackboard
    {
        public List<CharacterField> characters { get; private set; } = new();

        public DSCharacterBlackboard()
        {
            scrollable = true;
            title = "Characters";
            SetPosition(new Rect(20, 50, 200, 180));
            addItemRequested += (blackboard) =>
            {
                AddEmptyCharacterField();
            };
        }
        void AddEmptyCharacterField()
        {
            var characterField = new CharacterField("name");
            AddCharacterField( characterField);
        }

        public void AddCharacterField(CharacterField characterField)
        {
            this.Add(characterField);
            characters.Add(characterField);
        }

        public void RemoveCharacterField(CharacterField characterField)
        {
            this.Remove(characterField);
            characters.Remove(characterField);
        }

        public void ClearCharacters()
        {
            characters.ForEach(character => this.Remove(character));
            characters.Clear();
        }
    }
}
