using DS.Data;
using DS.ScriptableObjects;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Choices : MonoBehaviour
{
    [SerializeField] Button _button;
    [SerializeField] TextMeshProUGUI _text;

    DSDialogueSO _nextDialogue;
    Action<DSDialogueSO> actionOnClick;

    public void Initialization(DSDialogueChoiceData choiceData, Action<DSDialogueSO> action)
    {
        _nextDialogue = choiceData.NextDialogue;
        _text.text = choiceData.Text;
        actionOnClick = action;
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(ClickChoices);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ClickChoices);
    }

    public void ClickChoices()
    {
        actionOnClick.Invoke(_nextDialogue);
    }
}
