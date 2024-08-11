using DS.ScriptableObjects;
using UnityEngine;

public class DialogPanel : MonoBehaviour
{
    [SerializeField] DialogView dialogView;

    Character _character;
    StateMachineEvents<Character> _stateMachine;

    void Construct(Character character, StateMachineEvents<Character> stateMachine)
    {
        _character = character;
        _stateMachine = stateMachine;
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void StartDialog(DSDialogueSO _firstDialogue)
    {
        //переведем персонажа в диалоговый статус

        gameObject.SetActive(true);
        SetDialog(_firstDialogue);
    }


    private void SetDialog(DSDialogueSO _dialogue)
    {
        if (_dialogue != null)
            dialogView.ViewData(_dialogue, SetDialog);
        else
            EndDialog();
    }

    private void EndDialog()
    {
        gameObject.SetActive(false);
    }


}
