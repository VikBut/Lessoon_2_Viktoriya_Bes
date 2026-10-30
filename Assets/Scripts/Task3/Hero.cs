using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Hero : Character
{
    private Color colorA = Color.orange;
    private Color colorB = Color.blue;
    public HeroFighterType currentTypeHeroFigther;
    public Slider healthSlider;
    private int enemyDamage = 0;

    void Awake()
    {
        material = GetComponent<Renderer>().material;
        defaultColor = material.color;
        colorC = defaultColor;
        
        if (currentTypeHeroFigther == HeroFighterType.Mage)
        {
            material.color = colorB;
            healthPoint = 130;
        }
        else if (currentTypeHeroFigther == HeroFighterType.Warrior)
        {
            material.color = colorA;
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
    
    public void StartEffect()
    {
        StartCoroutine(ColorDamage());
    }

    public int TakeDamage()
    {
        return enemyDamage;
    }
    
    public int TakeHP()
    {
        return healthPoint;
    }

    private IEnumerator ColorDamage()
    {
        material.color = Color.red;

        yield return new WaitForSeconds(0.5f);

        material.color = defaultColor;
    }
    
    public void TakeDamage(int damage)
    {
        if (currentTypeHeroFigther == HeroFighterType.Mage)
        {
            int magRandom = Random.Range(10, 16);
            damage = magRandom;
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
                enemyDamage = Random.Range(6, 12);
            }
            else if (currentTypeHeroFigther == HeroFighterType.Warrior)
            {
                enemyDamage = Random.Range(10, 16);
            }
            
            other.GetComponent<Enemy>().GetDamage(enemyDamage);
            Debug.Log("Атака врага, урон:  " + enemyDamage);
        }
    }

}
