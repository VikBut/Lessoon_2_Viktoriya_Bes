using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] 
    private MagicType[] m_enemyMagicTypes;
    
    [SerializeField] 
    private MagicType[] m_weaponMagicTypes;
    
    [SerializeField] 
    private string[] m_heroNames;
    
    [SerializeField] 
    private string[] m_enemyNames;
    
    [SerializeField] 
    private string[] m_weaponNames;
    
    [SerializeField] 
    private int[] m_enemyArmors;
    
    [SerializeField] 
    private int[] m_weaponDamages;
    
    [SerializeField] 
    private int m_baseDamage;

    private CharacterTask4[] m_heroes;
    private CharacterTask4[] m_enemies;
    private WeaponTask4[] m_weapons;
    
    private void Start()
    {
        m_heroes = new CharacterTask4[m_heroNames.Length];
        m_enemies = new CharacterTask4[m_enemyNames.Length];
        m_weapons = new WeaponTask4[m_weaponNames.Length];
        
        for (int i = 0; i < m_heroes.Length; i++)
        {
            m_heroes[i] = new CharacterTask4(
                m_heroNames[i],
                0,
                MagicType.None
            );
        }
        
        for (int i = 0; i < m_enemies.Length; i++)
        {
            m_enemies[i] = new CharacterTask4(
                m_enemyNames[i],
                m_enemyArmors[i],
                m_enemyMagicTypes[i]
            );
        }

        for (int i = 0; i < m_weapons.Length; i++)
        {
            m_weapons[i] = new WeaponTask4(
                m_weaponNames[i],
                m_weaponDamages[i],
                m_weaponMagicTypes[i]
            );
        }
        
        int count = Mathf.Min(m_heroes.Length, m_enemies.Length, m_weapons.Length);

        for (int i = 0; i < count; i++)
        {
            m_heroes[i].Attack(m_baseDamage, m_weapons[i], m_enemies[i]);
            Debug.Log(m_enemies[i].Name + " | Здоровье: " + m_enemies[i].Health);
            
            if (!m_enemies[i].IsAlive)
            {
                Debug.Log(m_enemies[i].Name + " умер");
                continue;
            }
            
            m_enemies[i].Attack(m_baseDamage, m_weapons[i], m_heroes[i]);
            Debug.Log(m_heroes[i].Name + " | Здоровье: " + m_heroes[i].Health);

            if (!m_heroes[i].IsAlive)
            {
                Debug.Log(m_heroes[i].Name + " умер");
            }
        }
    }
}