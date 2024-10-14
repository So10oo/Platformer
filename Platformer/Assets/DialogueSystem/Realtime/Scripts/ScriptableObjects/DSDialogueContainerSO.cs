using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem.Realtime
{
    public class DSDialogueContainerSO : SerializedScriptableObject
    {
        [field: SerializeField,ReadOnly] public string FileName { get; set; }
        [field: SerializeField,ReadOnly] public /*Serializable*/Dictionary<DSDialogueGroupSO, List<DSDialogueSO>> DialogueGroups { get; set; }
        [field: SerializeField,ReadOnly] public List<DSDialogueSO> UngroupedDialogues { get; set; } 

        public static DSDialogueContainerSO CreateInstance() 
        {
            var instance = ScriptableObject.CreateInstance<DSDialogueContainerSO>();
            instance.DialogueGroups = new();
            instance.UngroupedDialogues = new();
            return instance;
        }
      
    }
}