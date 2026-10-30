using System;
using System.Collections;
using UnityEngine;

public class Arena : MonoBehaviour
{
    private Enemy _enemy;
    private Hero _hero;
    [SerializeField] private Transform _spawnPointEnemy;
    [SerializeField] private Transform _spawnPointHero;
    [SerializeField] private GameObject _prefabHero;
    [SerializeField] private GameObject _prefabEnemy;
    
    private int count = 0;

    private void Awake()
    {
        var hero = Instantiate(_prefabHero, _spawnPointHero.position,  Quaternion.identity);
        var enemy = Instantiate(_prefabEnemy, _spawnPointEnemy.position, Quaternion.identity);

        _enemy = enemy.GetComponent<Enemy>();
        _hero = hero.GetComponent<Hero>();
    }

    public void Start()
    {
        StartCoroutine(AnimationFight());
    }

    private IEnumerator AnimationFight()
    {
        PrintStartRound();
        
        yield return new WaitForSeconds(1.5f);
        
        while (!_enemy.IsDead() || !_hero.IsDead())
        {
            _hero.Fight();
            
            yield return new WaitForSeconds(1.25f);

            if (_enemy.IsDead())
            {
                break;
            }
            
            _enemy.Fight();
            
            yield return new WaitForSeconds(1.25f);
            
            if (_hero.IsDead())
            {
                break;
            }
            count++;
            PrintEndRound();
        }

        PrintEndGame();
    }

    private void PrintStartRound()
    {
        Debug.Log(_hero.currentTypeHeroFigther + " (" + _hero.TakeHP() + " HP) против " + _enemy.currentTypeEnemyFigther + " (" + _enemy.TakeHP() + " HP)");
    }

    private void PrintEndRound()
    {
        Debug.Log("Раунд " + count + " : " + _hero.currentTypeHeroFigther + " наносит "
                  + _hero.TakeDamage() + ", у противника " + _enemy.TakeHP() + " HP; " + _enemy.currentTypeEnemyFigther
                  + " наносит " + _enemy.TakeDamage() + " у героя " + _hero.TakeHP() + " HP");
    }

    private void PrintEndGame()
    {
        if (_enemy.IsDead())
        {
            Debug.Log("Герой победил! У него осталось " + _hero.TakeHP() + " HP");
        }
        else
        {
            Debug.Log("Враг победил! У него осталось  " + _hero.TakeHP() + " HP");
        }
    }
}