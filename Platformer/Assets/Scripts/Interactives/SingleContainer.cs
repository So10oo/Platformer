using TMPro;
using UnityEngine;

public class SingleContainer : Interactive
{
    [SerializeField] GameObject Object;
    [SerializeField] TextMeshPro _mes;

    public override void Interaction()
    {
        var _Object = Instantiate(Object);
        character.GetWeapon(_Object.GetComponent<Weapon>());
        Object = null;
       // return false;
    }

    protected override void IsPlayerInObjectValueChange(bool obj)
    {
        base.IsPlayerInObjectValueChange(obj);
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

