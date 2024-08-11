using UnityEngine;
using Zenject;

public abstract class Interactive : MonoBehaviour, IActionCharacter
{
    protected LayerCheck IsPlayerInObject;

    protected Character character;
    protected StateMachineEvents<Character> stateMachine;
    protected InputService inputService;

    [Inject]
    void Construct(Character character, InputService inputService, StateMachineEvents<Character> stateMachine)
    {
        this.character = character;
        this.inputService = inputService;
        this.stateMachine = stateMachine;
    }

    protected virtual void StartMonoBehavior()
    {
        IsPlayerInObject = GetComponent<LayerCheck>();
        IsPlayerInObject.ValueChandge += IsPlayerInObjectValueChange;
    }

    private void Start()
    {
        StartMonoBehavior();
    }

    protected virtual void IsPlayerInObjectValueChange(bool obj)
    {
        if (obj)
        {
            character.action = this;
        }
        else
        {
            character.action = null;
        }
        View();
    }

    protected abstract void View();


    public abstract void Interaction();
}

