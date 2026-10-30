using UnityEngine;

public class Calculator : MonoBehaviour
{
    [SerializeField] private ElementsGun _criticalDamage;
    [SerializeField] private EnemyScript _damageEnd;
    private int count = 0;
    
    private void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag("Enemy"))
        {
            count++;
        
            if (_criticalDamage.isCritical == true)
            {
                if (_criticalDamage.elementForCalc == true)
                {
                    Debug.Log("Удар " + count+ ", урон: " + _damageEnd.lastDamage + "\n" + "Критический урон: x2 и урон от стихии x1.5");
                }
                else
                {
                    Debug.Log("Удар " + count+ ", урон: " + _damageEnd.lastDamage + "\n" + "Критический урон: x2 и урона от стихии нет");
                }
            }
            else
            {
                if (_criticalDamage.elementForCalc == true)
                {
                    Debug.Log("Удар " + count+ ", урон: " + _damageEnd.lastDamage + "\n" + "Критический урон: нет и урон от стихии x1.5");
                }
                else
                {
                    Debug.Log("Удар " + count+ ", урон: " + _damageEnd.lastDamage + "\n" + "Критический урон: нет и урона от стихии нет");
                }
            }

            _damageEnd.lastDamage = 0;
        }
    }
}


