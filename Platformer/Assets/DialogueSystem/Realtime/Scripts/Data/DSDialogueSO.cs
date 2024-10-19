using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem.Realtime
{
    using Sirenix.OdinInspector;
#if UNITY_EDITOR
    using DialogueSystem.Editor;
    using System.Linq;
#endif

    public class DSDialogueSO : ScriptableObject
    {
        [field: SerializeField, ReadOnly]
        public string DialogueName { get; private set; }

        [field: SerializeField, ReadOnly]
        public CharacterDataSO Character { get; private set; }

        [field: TextArea(), SerializeField, ReadOnly]
        public string Text { get; private set; }

        [field: SerializeField, ReadOnly, TableList(AlwaysExpanded = true, DrawScrollView = false)]
        public List<DSDialogueChoiceData> Choices { get; private set; }

        [field: SerializeField, ReadOnly]
        public bool IsStartingDialogue { get; private set; }

#if UNITY_EDITOR  
        private static List<DSDialogueChoiceData> ConvertNodeChoicesToDialogueChoices(List<DSChoiceSaveData> nodeChoices)
        {
            var dialogueChoices = (from node in nodeChoices
                     select new DSDialogueChoiceData() { Text = node.Text })
                     .ToList();
            return dialogueChoices;
        }

        public static DSDialogueSO CreateInstance(DSNode node, Dictionary<string, CharacterDataSO> createdCharacter)
        {
            var instance = ScriptableObject.CreateInstance<DSDialogueSO>();
            instance.name = node.DialogueName;
            instance.DialogueName = node.DialogueName;
            instance.Text = node.Text;
            instance.Choices = ConvertNodeChoicesToDialogueChoices(node.Choices);
            instance.IsStartingDialogue = node.IsStartingNode();
            instance.Character = createdCharacter[node.Character.ID];
            return instance;
        }
#endif

    }
}