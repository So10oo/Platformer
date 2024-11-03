using TMPro;
using UnityEngine;
using Zenject;

public class AltarWings : Interactive
{
    [SerializeField] TextMeshPro _message;

    FlyingState _flying;
    StateMachineEvents<Character> _stateMachine;

    [Inject]
    void Construct(InputService inputService, StateMachineEvents<Character> stateMachine)
    {
        _flying = new FlyingState(character, stateMachine, inputService);
        _stateMachine = stateMachine;
    }

    protected void View()
    {
        bool isView = playerCheck.InLayer && !_flying.isActiveState;
        _message.text = isView ? "Press F" : "";
    }

    private void StateMachine_OnChangeState(State<Character> ps, State<Character> ns)
    {
        View();
    }

    protected override void IsPlayerInZoneValueChange(bool value)
    {
        base.IsPlayerInZoneValueChange(value);
        if (value)
            _stateMachine.OnChangeState += StateMachine_OnChangeState;
        else
            _stateMachine.OnChangeState -= StateMachine_OnChangeState;
        View();
    }
 
    protected override void Interaction()
    {
        if (!_flying.isActiveState)
        {
            _stateMachine.ChangeState(_flying);
            View();
        }
    }

}
