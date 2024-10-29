using System;

public class ActionCharacterEvents : IActionCharacter
{
    public event Action beforeAction;
    public event Action afterAction;

    Action action;
    public ActionCharacterEvents(Action action)
    {
        this.action = action;
    }

    public void Interaction()
    {
        beforeAction?.Invoke();
        action?.Invoke();
        afterAction?.Invoke();
    }
}
