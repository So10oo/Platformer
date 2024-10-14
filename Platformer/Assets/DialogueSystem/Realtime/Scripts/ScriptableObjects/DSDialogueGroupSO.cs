#if UNITY_EDITOR
using DialogueSystem.Editor;
#endif
using UnityEngine;

namespace DialogueSystem.Realtime
{
    public class DSDialogueGroupSO
    {
        [field: SerializeField] public string GroupName { get; set; }

      
#if UNITY_EDITOR
        public DSDialogueGroupSO(DSGroup group)
        {
            GroupName = group.title;
        }
#endif
    }
}