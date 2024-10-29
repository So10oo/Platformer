using TMPro;
using UnityEngine;
using Zenject;

public class AltarDash : SingleInteractive
{
    [SerializeField] TextMeshPro _message;
    [SerializeField] GameObject _glow;
    DashState _dashState;

    [Inject]
    void Construct(InputService inputService, StateMachineEvents<Character> stateMachine)
    {
        _dashState = new DashState(character, stateMachine, inputService);
    }

    protected override void Interaction()
    {
        character["dash"] = _dashState;//выдаем способность персонажу 
    }

    public override void AfterInteraction()
    {
        base.AfterInteraction();
        Destroy(_message.gameObject);
        Destroy(_glow);
    }

    protected override void IsPlayerInZoneValueChange(bool isPlayerInZone)
    {
        base.IsPlayerInZoneValueChange(isPlayerInZone);
        _message.text = isPlayerInZone ? "Get dash" : "";
    }
}
