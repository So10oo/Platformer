using DS.ScriptableObjects;
using System;
using TMPro;
using UnityEngine;

public class DialogView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _placeSpeech;
    [SerializeField] Transform _containerChoices;
    [SerializeField] Choices _buttonChoicesPrefab;

    public void ViewData(DSDialogueSO dialogue, Action<DSDialogueSO> callback)
    {
        _placeSpeech.text = dialogue.Text;
        for(int i = 0; i < _containerChoices.childCount; i++)
            Destroy(_containerChoices.GetChild(i).gameObject);
        foreach (var choice in dialogue.Choices) 
        {
            var choicePref = Instantiate(_buttonChoicesPrefab, _containerChoices);
            choicePref.Initialization(choice, callback);
        }
    }
}
