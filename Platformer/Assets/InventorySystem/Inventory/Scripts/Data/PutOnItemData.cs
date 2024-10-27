using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SingleItemData")]
    public class PutOnItemData : ItemData
    {
        [field: SerializeField] public TypePutOnItem Type { get; private set; }
        private void OnValidate()
        {
            CapacitySlot = 1;
        }
    }

    public enum TypePutOnItem
    {
        foot,//ступня
        leg,//ноги
        torso,//туловище
        head,//головной убор 
        leftHand,//левая рука 
        rightHand,//правая рука
    }
}
