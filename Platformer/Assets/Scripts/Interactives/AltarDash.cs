using TMPro;
using UnityEngine;

public class AltarDash : Interactive
{
    [SerializeField] TextMeshPro _mes;

    public override void Interaction()
    {
        if (character["dash"] == null)
        {
            character["dash"] = new DashState(character, stateMachine, inputService);//выдаем способность персонажу 
            character.action = null;//запрещаем интерактировать с этим местом
            View();
        } 
    }

    protected override void View()
    {
        if (IsPlayerInObject.Value && character["dash"] == null)
            _mes.text = "Get dash";
        else
            _mes.text = "";
    }
}
