using Task1;
using UnityEngine;

public class Calculator : MonoBehaviour
{
    private int count = 0;
    
    [SerializeField] 
    private ElementsGun m_criticalDamage; // Нет проверки на null(если не назанчен в инспекторе)
    
    [SerializeField] 
    private EnemyScript m_damageEnd;
    
    [SerializeField] 
    private int m_maxRounds; // не используется в коде
    
    private void OnTriggerExit(Collider other) // тут наверно OnTriggerEnter, хоть и не критично наверно, но тут выполняется условие только тогда, когда он уже выходит с удара, а не наносит удар
    {
        if (other.CompareTag("Enemy"))
        {
            count++;

            if (m_criticalDamage.isCritical == true) // можно просто  if (m_criticalDamage.isCritical) сработает с true
            {
                if (m_criticalDamage.elementForCalc == true) // Тоже сравнение с true
                {
                    Debug.Log("Удар " + count + ", урон: " + m_damageEnd.lastDamage + "\n" +  // Тут вроде как желательно делать интерполяцию ($"...{переменная}...}
                              "Критический урон: x2 и урон от стихии x1.5");
                }
                else
                {
                    Debug.Log("Удар " + count + ", урон: " + m_damageEnd.lastDamage + "\n" + // Дальше тоже
                              "Критический урон: x2 и урона от стихии нет");
                }
            }
            else
            {
                if (m_criticalDamage.elementForCalc == true) // Сравнение с true
                {
                    Debug.Log("Удар " + count + ", урон: " + m_damageEnd.lastDamage + "\n" +
                              "Критический урон: нет и урон от стихии x1.5");
                }
                else
                {
                    Debug.Log("Удар " + count + ", урон: " + m_damageEnd.lastDamage + "\n" +
                              "Критический урон: нет и урона от стихии нет");
                }
            }

            m_damageEnd.lastDamage = 0;
        }
    }
}


