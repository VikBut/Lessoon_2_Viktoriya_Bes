using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character
{
    protected Color colorD = Color.burlywood;
    protected Color colorF = Color.darkOliveGreen;
    protected Color colorG = Color.sienna;
    public EnemyFighterType currentTypeEnemyFigther;
    public Slider healthSlider;
    private int heroDamage = 0;

    void Awake()
    {
        material = GetComponent<Renderer>().material;
        defaultColor = material.color;
        colorC = defaultColor;
        
        if (currentTypeEnemyFigther == EnemyFighterType.Goblin)
        {
            material.color = colorD;
            healthPoint = 50;
        }
        else if (currentTypeEnemyFigther == EnemyFighterType.Orc)
        {
            material.color = colorF;
            healthPoint = 80;
        }
        else if (currentTypeEnemyFigther == EnemyFighterType.Troll)
        {
            material.color = colorG;
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
    
    public void StartEffect()
    {
        StartCoroutine(ColorDamage());
    }
    
    public int TakeDamage()
    {
        return heroDamage;
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
                heroDamage = Random.Range(10, 16);
            }
            else if (currentTypeEnemyFigther == EnemyFighterType.Orc)
            {
                heroDamage = Random.Range(5, 10);
            }
            else if (currentTypeEnemyFigther == EnemyFighterType.Troll)
            {
                heroDamage = Random.Range(8, 12);
            }
            
            other.GetComponent<Hero>().GetDamage(heroDamage);
            Debug.Log("Атака героя, урон:  " + heroDamage);
        }
    }

}
