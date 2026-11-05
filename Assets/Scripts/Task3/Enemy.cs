using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character
{
    private int m_heroDamage = 0;
    
    protected Color colorD = Color.burlywood; // Нейминг цветов 
    protected Color colorF = Color.darkOliveGreen; // <-
    protected Color colorG = Color.sienna; // <-
    
    public EnemyFighterType currentTypeEnemyFigther;
    public Slider healthSlider;

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
    
    public int TakeDamage()  // Тут метод по названию наносит урон, а по факту только его выводит
    {
        return m_heroDamage;
    }
    
    public int TakeHP() // тоже самое, что и сверху
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
                m_heroDamage = Random.Range(10, 16);  // Тут получается, что heroDamage - урон, который наносЯт герою, а не урон самого героя, тоже переименоать надо
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
            Debug.Log("Атака героя, урон:  " + m_heroDamage); // $""
        }
    }
}
