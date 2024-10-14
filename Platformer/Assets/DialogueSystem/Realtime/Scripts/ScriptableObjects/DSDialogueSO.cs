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
        public string DialogueName { get; set; }

        [field: TextArea(), SerializeField,ReadOnly]
        public string Text { get; set; }

        [field: SerializeField, ReadOnly, TableList(AlwaysExpanded = true, DrawScrollView = false)]
        public List<DSDialogueChoiceData> Choices { get; set; }

        [field: SerializeField, ReadOnly]
        public bool IsStartingDialogue { get; set; }

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

        public static DSDialogueSO CreateInstance(DSNode node)
        {
            var instance = ScriptableObject.CreateInstance<DSDialogueSO>();
            instance.DialogueName = node.DialogueName;
            instance.Text = node.Text;
            instance.Choices = ConvertNodeChoicesToDialogueChoices(node.Choices);
            instance.IsStartingDialogue = node.IsStartingNode();
            return instance;
        }
#endif

    }
}