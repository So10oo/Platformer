using TMPro;
using UnityEngine;

public class SingleContainer : Interactive
{
    [SerializeField] GameObject Object;
    [SerializeField] TextMeshPro _mes;

    protected override void Interaction()
    {
        var _Object = Instantiate(Object);
        character.GetWeapon(_Object.GetComponent<Weapon>());
        Object = null;
    }

    //protected void View()
    //{
    //    if (playerCheck.Value && Object != null)
    //    {
    //        _mes.text = "get weapon";
    //    }
    //    else
    //    {
    //        _mes.text = "";
    //    }
    //}

}

