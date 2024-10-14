using DialogueSystem.Realtime;
using TMPro;
using UnityEngine;
using Zenject;

public class Conversation : Interactive
{
    [SerializeField] TextMeshPro _textMeshPro;

    DSDialogueSO _firstDialogue;
    DialogPanel _progressDialog;

    [Inject]
    void Construct(DialogPanel progressDialog)
    {
        _progressDialog = progressDialog;
        _firstDialogue = GetComponent<DSDialogue>().FirstDialogue;
    }

    public override void Interaction()
    {
        _progressDialog.StartDialog(_firstDialogue);
    }

    protected override void View()
    {
        if (IsPlayerInObject.Value)
            _textMeshPro.text = "Interactive";
        else
            _textMeshPro.text = "";
    }
}

