using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character
{
    private Color colorGoblin = Color.burlywood;
    private Color colorOrc = Color.darkOliveGreen;
    private Color colorTroll = Color.sienna;
    private int m_heroDamage = 0;
    
    public EnemyFighterType currentTypeEnemyFigther;
    public Slider healthSlider;
    
    public int takeDamage
    {
        get
        {
            return m_heroDamage;
        }
        set
        {
            m_heroDamage = value;
        }
    }

    private void Awake()
    {
        material = GetComponent<Renderer>().material;

        if (currentTypeEnemyFigther == EnemyFighterType.Goblin)
        {
            material.color = colorGoblin;
            healthPoint = 50;
        }
        else if (currentTypeEnemyFigther == EnemyFighterType.Orc)
        {
            material.color = colorOrc;
            healthPoint = 80;
        }
        else if (currentTypeEnemyFigther == EnemyFighterType.Troll)
        {
            material.color = colorTroll;
            healthPoint = 140;
        }
        
        healthSlider.maxValue = healthPoint;
    }
    
    void Update()
    {
        healthSlider.value = healthPoint;
    }
    
    public void Fight()
    {
        if (IsDead())
        {
            return;
        }
        
        animator.Play("Enemy");
    }
    
    public bool IsDead()
    {
        return healthPoint <= 0;
    }

    public void GetDamage(int damage)
    {
        healthPoint -= damage;
    }
    
    public int TakeHP()
    {
        return healthPoint;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (IsDead())
        {
            return;
        }
        
        if (other.CompareTag("Hero"))
        {
            if (currentTypeEnemyFigther == EnemyFighterType.Goblin)
            {
                m_heroDamage = Random.Range(10, 16);
            }
            else if (currentTypeEnemyFigther == EnemyFighterType.Orc)
            {
                m_heroDamage = Random.Range(5, 10);
            }
            else if (currentTypeEnemyFigther == EnemyFighterType.Troll)
            {
                m_heroDamage = Random.Range(8, 12);
            }
            
            other.GetComponent<Hero>().GetDamage(m_heroDamage);
        }
    }
}