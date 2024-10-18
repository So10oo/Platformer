using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DialogueSystem.Realtime
{
    public class DSInspectorInitialDialogue : MonoBehaviour
    {
        [SerializeField]
        private DSDialogueContainerSO _dialogueContainer;

        [SerializeField, ShowIf("@_dialogueContainer!=null")]
        private bool groupedDialogues;

        [SerializeField, ShowIf("@_dialogueContainer!=null")]
        private bool startingDialoguesOnly;

        [ShowInInspector, ShowIf("@_dialogueContainer!=null"), ValueDropdown(nameof(GetDialogue))]
        public DSDialogueSO FirstDialogue { get; private set; }
         
#if UNITY_EDITOR
        private ValueDropdownList<DSDialogueSO> GetDialogue()
        {
            ValueDropdownList<DSDialogueSO> result = new();
            IEnumerable<DSDialogueSO> resultList = new List<DSDialogueSO>();
            if (_dialogueContainer != null)
            {
                var listAllDialogues = GetAllDialogueForGroup();
                if (!groupedDialogues && _dialogueContainer.UngroupedDialogues != null)
                    listAllDialogues.AddRange(_dialogueContainer.UngroupedDialogues);
                if (startingDialoguesOnly)
                {
                    resultList = from d in listAllDialogues
                                 where d.IsStartingDialogue == true
                                 select d;
                }
                else
                {
                    resultList = listAllDialogues;
                }
                foreach (var dialogue in resultList)
                {
                    //если одинаковые имена диалогов то высвечиваются только 1 
                    result.Add(dialogue.DialogueName, dialogue);
                }
            }
            return result;
        }

        List<DSDialogueSO> GetAllDialogueForGroup()
        {
            if (_dialogueContainer.DialogueGroups != null)
            {
                var list = new List<DSDialogueSO>();
                foreach (var group in _dialogueContainer.DialogueGroups)
                    list.AddRange(group.Value);
                return list;
            }
            return new List<DSDialogueSO>();
        }
#endif
    }
}