using UnityEngine;


public abstract class SingleInteractive : Interactive
{
    Collider2D _collider;

    protected override void StartMonoBehavior()
    {
        base.StartMonoBehavior();
        _collider = GetComponent<Collider2D>();
    }

    public override void AfterInteraction()
    {
        Destroy(_collider);
        Destroy(playerCheck);
        Destroy(this);
        character.action = null;
    }
}

