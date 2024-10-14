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
        //[field: NonSerialized] //SerializeField = fatal error :)
        public DSDialogueSO NextDialogue { get; set; }
    }
} 