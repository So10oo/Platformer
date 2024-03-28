using System.Collections.Generic;

public class DictionaryStates : Dictionary<string, BaseCharacterState>
{
    Character _character;
    IInputService _inputService;
    StateMachine<Character> _stateMachine;

    public new BaseCharacterState this[string key]
    {
        get
        {
            BaseCharacterState t;
            return base.TryGetValue(key, out t) ? t : null;
        }
        set { base[key] = value; }
    }

    public DictionaryStates(Character character, IInputService inputService, StateMachine<Character> stateMachine)
    {
        _inputService = inputService;
        _stateMachine = stateMachine;
        _character = character;
        Add("standing", new StandingState(_character, _stateMachine, _inputService));
        Add("jumping", new JumpingState(_character, _stateMachine, _inputService));
        Add("freeFall", new FreeFallState(_character, _stateMachine, _inputService));
        Add("moving", new MovingState(_character, _stateMachine, _inputService));
    }
}

