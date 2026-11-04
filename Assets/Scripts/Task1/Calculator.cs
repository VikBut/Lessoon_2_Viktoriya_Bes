using Task1;
using UnityEngine;

public class Calculator : MonoBehaviour
{
    private int count = 0;
    
    [SerializeField] 
    private ElementsGun m_criticalDamage;
    
    [SerializeField] 
    private EnemyScript m_damageEnd;
    
    [SerializeField] 
    private int m_maxRounds;
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            count++;

            if (m_criticalDamage.isCritical == true)
            {
                if (m_criticalDamage.elementForCalc == true)
                {
                    Debug.Log("Удар " + count + ", урон: " + m_damageEnd.lastDamage + "\n" +
                              "Критический урон: x2 и урон от стихии x1.5");
                }
                else
                {
                    Debug.Log("Удар " + count + ", урон: " + m_damageEnd.lastDamage + "\n" +
                              "Критический урон: x2 и урона от стихии нет");
                }
            }
            else
            {
                if (m_criticalDamage.elementForCalc == true)
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


