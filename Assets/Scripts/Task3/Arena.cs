using System.Collections;
using UnityEngine;

public class Arena : MonoBehaviour
{
    [SerializeField] 
    private int m_roundsLimit = 5;
    
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

    private void Awake()
    {
        InstantiateCharacters();
    }

    public void Start()
    {
        StartCoroutine(AnimationFight());
    }

    private IEnumerator AnimationFight()
    {
        PrintStartRound();
        
        yield return new WaitForSeconds(1.5f);
        
        int currentRound = 0;
        
        while ((!m_enemy.IsDead() || !m_hero.IsDead()) && m_roundsLimit > currentRound)
        {
            currentRound++;
            m_hero.Fight();
            
            yield return new WaitForSeconds(1.25f);

            if (m_enemy.IsDead())
            {
                PrintEndRound(currentRound);
                break;
            }
            
            m_enemy.Fight();
            
            yield return new WaitForSeconds(1.25f);
            
            if (m_hero.IsDead())
            {
                PrintEndRound(currentRound);
                break;
            }

            PrintEndRound(currentRound);
            ResetCharacterStatistics();
        }

        PrintEndGame();
    }

    private void PrintStartRound()
    {
        Debug.Log(m_hero.currentTypeHeroFigther + " (" + m_hero.TakeHP() + " HP) против " + 
                  m_enemy.currentTypeEnemyFigther + " (" + m_enemy.TakeHP() + " HP)");
    }

    private void PrintEndRound(int currentRound)
    {
        Debug.Log("Раунд " + currentRound + " : " + m_hero.currentTypeHeroFigther + " наносит "
                  + m_hero.takeDamage + ", у противника " + Mathf.Max(0, m_enemy.TakeHP()) + " HP; " + 
                  m_enemy.currentTypeEnemyFigther + " наносит " + m_enemy.takeDamage + " у героя " + 
                  Mathf.Max(m_hero.TakeHP()) + " HP");
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

    private void ResetCharacterStatistics()
    {
        m_enemy.takeDamage = 0;
        m_hero.takeDamage = 0;
    }

    private void InstantiateCharacters()
    {
        var hero = Instantiate(m_prefabHero, m_spawnPointHero.position,  Quaternion.identity);
        var enemy = Instantiate(m_prefabEnemy, m_spawnPointEnemy.position, Quaternion.identity);

        m_enemy = enemy.GetComponent<Enemy>();
        m_hero = hero.GetComponent<Hero>();
    }
}