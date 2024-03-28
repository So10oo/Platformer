using TMPro;
using UnityEngine;

public class getDash : Interactive
{
    [SerializeField] TextMeshPro _mes;

    [SerializeField] View<bool> view;

    protected override bool? Interaction()
    {
        if (character.states["dash"] == null)
        {
            character.states["dash"] = new DashState(character, stateMachine, InputService);//������ ����������� ��������� 
            character.Interaction -= null;//��������� ��������������� � ���� ������
            View();
        } 
        return null;
    }

    protected override void View()
    {
        if (IsPlayerInObject.Value && character.states["dash"] == null)
            _mes.text = "Get dash";
        else
            _mes.text = "";
    }
}
