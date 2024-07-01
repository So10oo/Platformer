using UnityEngine;

public class WeaponSlot : MonoBehaviour
{
    [SerializeField] Weapon _weapon;
    
    public Weapon weapon => _weapon;

    public void SetWeapon(Weapon weapon)
    {
        if (_weapon != null)
            Destroy(_weapon.gameObject);
        var w = Instantiate(weapon, transform, true);
        _weapon = w;
    }

    private void Awake()
    {
        _weapon = GetComponentInChildren<Weapon>();
    }
}
