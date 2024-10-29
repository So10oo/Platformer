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
        _firstDialogue = GetComponent<DSInspectorInitialDialogue>().FirstDialogue;
    }

    protected override void Interaction()
    {
        _progressDialog.StartDialog(_firstDialogue);
    }

    protected override void IsPlayerInZoneValueChange(bool isPlayerInZone)
    {
        base.IsPlayerInZoneValueChange(isPlayerInZone);
        _textMeshPro.text = isPlayerInZone ? "Interactive" : "";
    }
}

