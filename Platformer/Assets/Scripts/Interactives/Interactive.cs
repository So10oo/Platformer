using UnityEngine;
using Zenject;

public abstract class Interactive : MonoBehaviour
{
    protected LayerCheck IsPlayerInObject;

    protected Character character;
    protected StateMachineEvents<Character> stateMachine;
    protected IInputService InputService;
    [Inject]
    void Construct(Character _character, IInputService inputService, StateMachineEvents<Character> _stateMachine)
    {
        character = _character;
        InputService = inputService;
        stateMachine = _stateMachine;
    }

    protected virtual void StartMonoBehaviour()
    {
        IsPlayerInObject = GetComponent<LayerCheck>();
        IsPlayerInObject.ValueChandge += IsPlayerInObjectValueChandge;
    }

    private void Start()
    {
        StartMonoBehaviour();
    }

    protected virtual void IsPlayerInObjectValueChandge(bool obj)
    {
        if (obj)
        {
            character.Interaction += Interaction;
        }
        else
        {
            character.Interaction -= Interaction;
        }
        View();
    }

    protected abstract bool? Interaction();
    protected abstract void View();
}

