using TMPro;
using UnityEngine;

public class AltarDash : Interactive
{
    [SerializeField] TextMeshPro _mes;

    [SerializeField] View<bool> view;

    public override void Interaction()
    {
        if (character["dash"] == null)
        {
            character["dash"] = new DashState(character, stateMachine, InputService);//������ ����������� ��������� 
            character.action = null;//��������� ��������������� � ���� ������
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