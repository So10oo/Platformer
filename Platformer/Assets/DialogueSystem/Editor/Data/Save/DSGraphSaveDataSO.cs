using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem.Editor
{
    public class DSGraphSaveDataSO : ScriptableObject
    {
        [field: SerializeField,ReadOnly] public string FileName { get; set; }
        [field: SerializeField,ReadOnly] public List<DSGroupSaveData> Groups { get; set; } = new();
        [field: SerializeField,ReadOnly] public List<DSNodeSaveData> Nodes { get; set; } = new();

        public static DSGraphSaveDataSO CreateInstance()
        {
            var instance = ScriptableObject.CreateInstance<DSGraphSaveDataSO>();
            instance.Groups = new();
            instance.Nodes = new();
            return instance;
        }

        [Button()]
        void OpenGroup()
        {
            var window = DSEditorWindow.Open();
            window.Load(this);
        }

    }
}