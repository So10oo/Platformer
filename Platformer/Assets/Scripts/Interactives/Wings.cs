using TMPro;
using UnityEngine;

public class Wings : Interactive 
{
    [SerializeField] TextMeshPro _mes;


    protected override void View()
    {
        if (IsPlayerInObject.Value && !flying.isActiveState)
            _mes.text = "Press F";
        else
            _mes.text = "";
    }


    protected override void StartMonoBehavior()
    {
        base.StartMonoBehavior();
        flying = new FlyingState(character, stateMachine, inputService);
    }

    private void StateMachine_OnChangeState(State<Character> ps, State<Character> ns)
    {
        View();
    }

    protected override void IsPlayerInObjectValueChange(bool value)
    {
        if (value)
        {
            stateMachine.OnChangeState += StateMachine_OnChangeState;
            character.action = this;
        }
        else
        {
            character.action = null;
            stateMachine.OnChangeState -= StateMachine_OnChangeState;
        }
        View();
    }

    FlyingState flying;
    public override void Interaction()
    {
        if (!flying.isActiveState)
        {
            stateMachine.ChangeState(flying);
            View();
            //return true;
        }
        //return false;
    }


}
