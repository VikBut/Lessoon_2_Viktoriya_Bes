using UnityEngine;

public class CharacterTask4
{
    private const float ChanceCriticalDamage = 0.3f;
    private const float MagicDamage = 1.5f;
    private const int MaxHealth = 100;
    
    private MagicType m_targetElementType;
    private int m_armor = 3;
    
    public bool IsCritical { get; private set; }
    public string Name { get; }
    public int Health { get; private set; } = MaxHealth;
    public bool IsAlive => Health > 0;
    
    public CharacterTask4(string name, int armor, MagicType mTargetElementType)
    {
        Name = string.IsNullOrEmpty(name) ? "Unknown" : name;
        this.m_armor = Mathf.Max(armor, 0);
        this.m_targetElementType = mTargetElementType;
    }
    
    public void Heal(int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        Health = Mathf.Clamp(Health + amount, 0, MaxHealth);
    }
    
    public void TakeDamage(int damage, MagicType weaponElement, float criticalChance)
    {
        if (damage <= 0 || !IsAlive) 
        {
            return;
        }
        
        IsCritical = false;
        float randomFloat = Random.Range(0f, 1f);
        int damageReceived = Mathf.Max(1, damage - m_armor);

        if (weaponElement == MagicType.None || m_targetElementType == MagicType.None)
        {
            Debug.Log("Стихии совпадают, урон не меняется");
        }
        else if (weaponElement == m_targetElementType)
        {
            damageReceived = 0;
        }
        else if ((weaponElement == MagicType.Fire && m_targetElementType == MagicType.Ice) || (weaponElement == MagicType.Ice && m_targetElementType == MagicType.Fire))
        {
            damageReceived = Mathf.FloorToInt(damageReceived * MagicDamage);
        }
        
        if (randomFloat < ChanceCriticalDamage && damageReceived > 0)
        {
            IsCritical = true;
            damageReceived *= 2;
            Debug.Log("Критический урон");
        }
        
        Health = Mathf.Max(Health - damageReceived, 0);
        Debug.Log(Name + " получил " + damageReceived + " урона");
    }

    public void Attack(int baseDamage, WeaponTask4 weapon, CharacterTask4 target)
    {
        Debug.Log(Name + " атакует " + target.Name + " с оружием " + weapon.weaponName);
        int totalDamage = baseDamage + weapon.damage;
        target.TakeDamage(totalDamage, weapon.weaponType, ChanceCriticalDamage);
    }
}