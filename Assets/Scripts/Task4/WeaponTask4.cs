using UnityEngine;

public class WeaponTask4
{
    public MagicType weaponType { get; }
    public string weaponName { get; }
    public int damage { get; }

    public WeaponTask4(string weaponName, int damage, MagicType weaponType)
    {
        this.weaponName = string.IsNullOrEmpty(weaponName) ? "Unknown" : weaponName;
        this.damage = Mathf.Max(damage, 0);
        this.weaponType = weaponType;
    }
}