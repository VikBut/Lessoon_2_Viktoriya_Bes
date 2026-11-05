using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Hero : Character
{
    private Color m_colorA = Color.orange; // Названия цветов
    private Color m_colorB = Color.blue; // Тоже
    private int m_enemyDamage = 0; // Опять же как с файлом Enemy, скорее всего не урон противника, а урон по противнику
    
    public HeroFighterType currentTypeHeroFigther;
    public Slider healthSlider;
    
    void Awake()
    {
        material = GetComponent<Renderer>().material;
        defaultColor = material.color;
        colorC = defaultColor;
        
        if (currentTypeHeroFigther == HeroFighterType.Mage)
        {
            material.color = m_colorB;
            healthPoint = 130;
        }
        else if (currentTypeHeroFigther == HeroFighterType.Warrior)
        {
            material.color = m_colorA;
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
    
    public void StartEffect() // Вроде не используется
    {
        StartCoroutine(ColorDamage());
    }

    public int TakeDamage() // Возвращает число, а по названию наносит урон
    {
        return m_enemyDamage;
    }
    
    public int TakeHP() // Тоже
    {
        return healthPoint;
    }

    private IEnumerator ColorDamage()
    {
        material.color = Color.red;

        yield return new WaitForSeconds(0.5f);

        material.color = defaultColor;
    }
    
    public void TakeDamage(int damage) // Второе название TakeDamage, поидее конфликтов быть не должно, но одинаковые названия лучше заменить(прошлый метод сам напрашивается на переименование)
    {
        if (currentTypeHeroFigther == HeroFighterType.Mage)
        {
            int magRandom = Random.Range(10, 16);
            damage = magRandom; // Можно просто сделать damage = Random.Range
            healthPoint -= damage;
        }
        else if (currentTypeHeroFigther == HeroFighterType.Warrior)
        {
            int warRandom = Random.Range(6, 12);
            damage = warRandom;
            healthPoint -= damage;
        }
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
            
            other.GetComponent<Enemy>().GetDamage(m_enemyDamage); // тоже нет проверки на отрицательное и ноль
            Debug.Log("Атака врага, урон:  " + m_enemyDamage); //$""
        }
    }
}
