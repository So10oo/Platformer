using UnityEngine;

public class Plaque : View<bool>
{
    public override void ViewData(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}

