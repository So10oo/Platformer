using UnityEngine;
using Zenject;

public abstract class Interactive : MonoBehaviour
{
    protected LayerCheck playerCheck;
    protected Character character;

    ActionCharacterEvents _interactive;

    [Inject]
    void Construct(Character character)
    {
        this.character = character;
    }

    private void Start() => StartMonoBehavior();

    protected virtual void StartMonoBehavior()
    {
        playerCheck = GetComponent<LayerCheck>();
        playerCheck.ValueChange += IsPlayerInZoneValueChange;
        _interactive = new ActionCharacterEvents(Interaction);
        _interactive.beforeAction += BeforeInteraction;
        _interactive.afterAction += AfterInteraction;
    }

    protected virtual void IsPlayerInZoneValueChange(bool isPlayerInZone) => character.action = isPlayerInZone ? _interactive : null;
    
    protected abstract void Interaction();

    public virtual void BeforeInteraction()
    {

    }

    public virtual void AfterInteraction()
    {

    }
}

 