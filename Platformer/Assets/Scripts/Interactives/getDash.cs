using TMPro;
using UnityEngine;

public class getDash : Interactive
{
    [SerializeField] TextMeshPro _mes;

    [SerializeField] View<bool> view;

    protected override bool? Interaction()
    {
        if (character["dash"] == null)
        {
            character["dash"] = new DashState(character, stateMachine, InputService);//������ ����������� ��������� 
            character.Interaction -= null;//��������� ��������������� � ���� ������
            View();
        } 
        return null;
    }

    protected override void View()
    {
        if (IsPlayerInObject.Value && character["dash"] == null)
            _mes.text = "Get dash";
        else
            _mes.text = "";
    }
}
