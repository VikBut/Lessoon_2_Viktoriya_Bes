using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Hero : Character
{
    private Color m_colorFire = Color.orange;
    private Color m_colorIce = Color.blue;
    private int m_enemyDamage = 0;
    
    public HeroFighterType currentTypeHeroFigther;
    public Slider healthSlider;
    
    public int takeDamage
    {
        get
        {
            return  m_enemyDamage;
        }
        set
        {
            m_enemyDamage = value;
        }
    }
    
    void Awake()
    {
        material = GetComponent<Renderer>().material;

        if (currentTypeHeroFigther == HeroFighterType.Mage)
        {
            material.color = m_colorIce;
            healthPoint = 130;
        }
        else if (currentTypeHeroFigther == HeroFighterType.Warrior)
        {
            material.color = m_colorFire;
            healthPoint = 100;
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
        
        animator.Play("Hero");
    }
    
    public void GetDamage(int damage)
    {
        healthPoint -= damage;
    }
    
    public bool IsDead()
    {
        return healthPoint <= 0;
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
        
        if (other.CompareTag("Enemy"))
        {
            if (currentTypeHeroFigther == HeroFighterType.Mage)
            { 
                m_enemyDamage = Random.Range(6, 12);
            }
            else if (currentTypeHeroFigther == HeroFighterType.Warrior)
            {
                m_enemyDamage = Random.Range(10, 16);
            }
            
            other.GetComponent<Enemy>().GetDamage(m_enemyDamage);
        }
    }
}