using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem.Realtime
{
    using Sirenix.OdinInspector;
#if UNITY_EDITOR
    using DialogueSystem.Editor;
#endif

    public class DSDialogueSO : ScriptableObject
    {
        [field: SerializeField, ReadOnly]
        public string DialogueName { get; private set; }

        [field: SerializeField, ReadOnly]
        public CharacterData Character { get; private set; }

        [field: TextArea(), SerializeField, ReadOnly]
        public string Text { get; private set; }

        [field: SerializeField, ReadOnly, TableList(AlwaysExpanded = true, DrawScrollView = false)]
        public List<DSDialogueChoiceData> Choices { get; private set; }

        [field: SerializeField, ReadOnly]
        public bool IsStartingDialogue { get; private set; }

#if UNITY_EDITOR  
        private static List<DSDialogueChoiceData> ConvertNodeChoicesToDialogueChoices(List<DSChoiceSaveData> nodeChoices)
        {
            var dialogueChoices = new List<DSDialogueChoiceData>();
            foreach (DSChoiceSaveData nodeChoice in nodeChoices)
            {
                var choiceData = new DSDialogueChoiceData()
                {
                    Text = nodeChoice.Text
                };
                dialogueChoices.Add(choiceData);
            }
            return dialogueChoices;
        }

        public static DSDialogueSO CreateInstance(DSNode node, List<CharacterData> datas)
        {
            var instance = ScriptableObject.CreateInstance<DSDialogueSO>();
            instance.name = node.DialogueName;
            instance.DialogueName = node.DialogueName;
            instance.Text = node.Text;
            instance.Choices = ConvertNodeChoicesToDialogueChoices(node.Choices);
            instance.IsStartingDialogue = node.IsStartingNode();
            instance.Character = datas.Find(x => x.ID == node.Character.ID);//CharacterData.CreateInstance(node.Character);
            return instance;
        }
#endif

    }
}