using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DialogueSystem.Realtime
{
    public class DSDialogue : MonoBehaviour
    {
        [SerializeField]
        private DSDialogueContainerSO dialogueContainer;

        [SerializeField, ShowIf("dialogueContainerIsNotNull")]
        private bool groupedDialogues;

        [SerializeField, ShowIf("dialogueContainerIsNotNull")]
        private bool startingDialoguesOnly;

        [/*field: SerializeField*/ShowInInspector, ShowIf("dialogueContainerIsNotNull"), ValueDropdown(nameof(GetDialogue))/*, HideLabel*/]
        public DSDialogueSO FirstDialogue { get; /*private*/ set; }
         
        private bool dialogueContainerIsNotNull => dialogueContainer != null;

#if UNITY_EDITOR
        private ValueDropdownList<DSDialogueSO> GetDialogue()
        {
            ValueDropdownList<DSDialogueSO> result = new();
            IEnumerable<DSDialogueSO> resultList = new List<DSDialogueSO>();

            var listAllDialogues = GetAllDialogueForGroup();
            if (!groupedDialogues && dialogueContainer.UngroupedDialogues != null) 
                listAllDialogues.AddRange(dialogueContainer.UngroupedDialogues);
            if (startingDialoguesOnly)
            {
                resultList = from d in listAllDialogues
                             where d.IsStartingDialogue = true
                             select d;
            }
            else
            {
                resultList = listAllDialogues;
            }


            foreach (var dialogue in resultList)
            {//если одинаковые имена диалогов то высвечиваются только 1 
                result.Add(dialogue.DialogueName, dialogue);
            }
               

            return result;
        }

        List<DSDialogueSO> GetAllDialogueForGroup()
        {
            if (dialogueContainer.DialogueGroups != null)
            {
                var list = new List<DSDialogueSO>();
                foreach (var group in dialogueContainer.DialogueGroups)
                    list.AddRange(group.Value);
                return list;
            }
            return new List<DSDialogueSO>();
        }


#endif
    }
}