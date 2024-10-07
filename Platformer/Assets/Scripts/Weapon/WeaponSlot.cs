using UnityEngine;

public class WeaponSlot : MonoBehaviour
{
    [SerializeField] Weapon _weapon;
    
    public Weapon weapon => _weapon;

    public void SetWeapon(Weapon weapon)
    {
        if (_weapon != null)
            Destroy(_weapon.gameObject);
        _weapon = Instantiate(weapon, transform, false);
    }

    private void Awake()
    {
        _weapon = GetComponentInChildren<Weapon>();
    }
}
