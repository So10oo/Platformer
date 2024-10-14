using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace DialogueSystem.Realtime
{
    [System.Serializable]
    public class DSDialogueChoiceData
    {
        [field: SerializeField] 
        public string Text { get; set; }
         
        [HideInInspector]
        [field: SerializeField]
        public DSDialogueSO NextDialogue { get; set; }
    }
} 