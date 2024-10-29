using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int itemId { get; private set; }
    public string  itemName { get; private set; }
    
    public virtual void UseItem(){}
    
    
}
