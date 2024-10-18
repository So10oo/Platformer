using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace DialogueSystem.Editor
{
    public partial class DSGraphView
    {
        Blackboard _blackboard;
        public List<CharacterField> characters { get; private set; } = new();

        private void AddBlackboard()
        {
            _blackboard = new Blackboard(this)
            {
                scrollable = true,
                title = "Characters"
            };
            _blackboard.SetPosition(new Rect(20, 50, 200, 180));
            _blackboard.addItemRequested += (blackboard) =>
            {
                var characterField = new CharacterField("name");
                blackboard.Add(characterField);
                characters.Add(characterField);
            };
            Add(_blackboard);
        }
    }
}
