using DialogueSystem.Editor;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DialogueSystem.Realtime
{
    public class DSDialogueContainerSO : SerializedScriptableObject
    {
        [field: SerializeField, ReadOnly] public string FileName { get; set; }
        [field: SerializeField, ReadOnly] public /*Serializable*/Dictionary<DSDialogueGroupSO, List<DSDialogueSO>> DialogueGroups { get; set; }
        [field: SerializeField, ReadOnly] public List<DSDialogueSO> UngroupedDialogues { get; set; }
        [field: SerializeField, ReadOnly] public List<CharacterDataSO> Characters { get; set; }

        public static DSDialogueContainerSO CreateInstance()
        {
            var instance = ScriptableObject.CreateInstance<DSDialogueContainerSO>();
            instance.DialogueGroups = new();
            instance.UngroupedDialogues = new();
            instance.Characters = new();
            return instance;
        }

        [Button()]
        void OpenGraph()
        {
            var window = DSEditorWindow.Open();
            foreach (var child in AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(this)))
            {
                if (child is DSGraphSaveDataSO graph)
                {
                    window.Load(graph);
                    return;
                }
            }
        }
    }
}