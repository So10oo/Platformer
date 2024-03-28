using TMPro;
using UnityEngine;

public class SingleContainer : Interactive
{
    [SerializeField] GameObject Object;
    [SerializeField] TextMeshPro _mes;

    protected override bool? Interaction()
    {
        var _Object = Instantiate(Object);
        character.GetWeapon(_Object.GetComponent<Weapon>());
        Object = null;
        return false;
    }

    protected override void IsPlayerInObjectValueChandge(bool obj)
    {
        base.IsPlayerInObjectValueChandge(obj);
    }

    protected override void View()
    {
        if (IsPlayerInObject.Value && Object != null)
        {
            _mes.text = "get weapon";
        }
        else
        {
            _mes.text = "";
        }
    }

}

