using System.Collections;
using UnityEngine;

public class Arena : MonoBehaviour
{
    [SerializeField] 
    private Transform m_spawnPointEnemy;
    
    [SerializeField] 
    private Transform m_spawnPointHero;
    
    [SerializeField] 
    private GameObject m_prefabHero;
    
    [SerializeField] 
    private GameObject m_prefabEnemy;
    
    private Enemy m_enemy;
    private Hero m_hero;
    private int m_count = 0;

    private void Awake()
    {
        var hero = Instantiate(m_prefabHero, m_spawnPointHero.position,  Quaternion.identity);
        var enemy = Instantiate(m_prefabEnemy, m_spawnPointEnemy.position, Quaternion.identity);

        m_enemy = enemy.GetComponent<Enemy>();
        m_hero = hero.GetComponent<Hero>();
    }

    public void Start()
    {
        StartCoroutine(AnimationFight());
    }

    private IEnumerator AnimationFight()
    {
        PrintStartRound();
        
        yield return new WaitForSeconds(1.5f);
        
        while (!m_enemy.IsDead() || !m_hero.IsDead())
        {
            m_hero.Fight();
            
            yield return new WaitForSeconds(1.25f);

            if (m_enemy.IsDead())
            {
                break;
            }
            
            m_enemy.Fight();
            
            yield return new WaitForSeconds(1.25f);
            
            if (m_hero.IsDead())
            {
                break;
            }
            m_count++;
            PrintEndRound();
        }

        PrintEndGame();
    }

    private void PrintStartRound()
    {
        Debug.Log(m_hero.currentTypeHeroFigther + " (" + m_hero.TakeHP() + " HP) против " + m_enemy.currentTypeEnemyFigther
                  + " (" + m_enemy.TakeHP() + " HP)");
    }

    private void PrintEndRound()
    {
        Debug.Log("Раунд " + m_count + " : " + m_hero.currentTypeHeroFigther + " наносит "
                  + m_hero.TakeDamage() + ", у противника " + m_enemy.TakeHP() + " HP; " + m_enemy.currentTypeEnemyFigther
                  + " наносит " + m_enemy.TakeDamage() + " у героя " + m_hero.TakeHP() + " HP");
    }

    private void PrintEndGame()
    {
        if (m_enemy.IsDead())
        {
            Debug.Log("Герой победил! У него осталось " + m_hero.TakeHP() + " HP");
        }
        else
        {
            Debug.Log("Враг победил! У него осталось  " + m_hero.TakeHP() + " HP");
        }
    }
}