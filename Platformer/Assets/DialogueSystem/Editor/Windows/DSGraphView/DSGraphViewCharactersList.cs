using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace DialogueSystem.Editor
{
    public partial class DSGraphView
    {
        //Blackboard _blackboard;

        //public List<CharacterField> characters { get; private set; } = new();

        //private void AddBlackboard()
        //{
        //    _blackboard = new Blackboard(this)
        //    {
        //        scrollable = true,
        //        title = "Characters"
        //    };
        //    _blackboard.SetPosition(new Rect(20, 50, 200, 180));
        //    _blackboard.addItemRequested += (blackboard) =>
        //    {
        //        AddEmptyCharacterField(blackboard);
        //    };
        //    Add(_blackboard);
        //}

        //void AddEmptyCharacterField(Blackboard blackboard)
        //{
        //    var characterField = new CharacterField("name");
        //    AddCharacterField(blackboard, characterField);
        //}

        //void AddCharacterField(Blackboard blackboard, CharacterField characterField)
        //{
        //    blackboard.Add(characterField);
        //    characters.Add(characterField);
        //}

        //void ClearCharacters()
        //{
        //    characters.ForEach(character => _blackboard.Remove(character));
        //    characters.Clear();
        //}
    }
}
