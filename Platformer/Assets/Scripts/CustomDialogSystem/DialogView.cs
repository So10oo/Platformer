using DialogueSystem.Realtime;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _placeSpeech;
    [SerializeField] Transform _containerChoices;
    [SerializeField] Choices _buttonChoicesPrefab;
    [SerializeField] Image _characterIcon;
    [SerializeField] TextMeshProUGUI _characterName;

    public void ViewData(DSDialogueSO dialogue, Action<DSDialogueSO> callback)
    {
        _placeSpeech.text = dialogue.Text;
        var texture = dialogue.Character.Icon;
        _characterIcon.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        _characterName.text = dialogue.Character.Name;  


        for (int i = 0; i < _containerChoices.childCount; i++)
            Destroy(_containerChoices.GetChild(i).gameObject);
        foreach (var choice in dialogue.Choices) 
        {
            var choicePref = Instantiate(_buttonChoicesPrefab, _containerChoices);
            choicePref.Initialization(choice, callback);
        }
    }
}
