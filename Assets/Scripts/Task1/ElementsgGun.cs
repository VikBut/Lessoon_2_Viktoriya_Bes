using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ElementsGun : MonoBehaviour
{
    private const float MagicDamage = 1.5f;

    public int damageAmount;

    private Material material;
    private Color colorA = Color.orange;
    private Color colorB = Color.blue;
    private Color defaultColor;
    private Color endColor;
    private float randomValue;
    public MagicType currentElement;
    public int amountForCalc;


    private const float ChanceCriticalDamage = 0.3f;
    [NonSerialized] public bool isCritical;
    [NonSerialized] public bool elementForCalc;
    [NonSerialized] public int damageReceived;


    public void Start()
    {
        material = GetComponent<Renderer>().material;
        defaultColor = material.color;

    }

    public Color ApplyColor(MagicType newElement)
    {
        switch (newElement)
        {
            case MagicType.Fire:
            {
                return colorA;
            }
            case MagicType.Ice:
            {
                return colorB;
            }
            case MagicType.None:
            {
                return defaultColor;
            }
        }

        return default;
    }

    private void OnValidate()
    {
        if (material != null && Application.isPlaying)
        {

            material.color = ApplyColor(currentElement);
        }
    }
    

    private void OnTriggerEnter(Collider other)
    {
        
        elementForCalc = false;
        damageReceived = damageAmount;
        

        if (other.CompareTag("Enemy"))
        {
            EnemyScript enemyScript = other.GetComponent<EnemyScript>();
            MagicType enemyMagicType = enemyScript.currentElementEnemy;

            if (currentElement == MagicType.None || enemyMagicType == MagicType.None)
            {
                CriticalDamage(damageReceived);
                enemyScript.TakeDamage(amountForCalc);
            }
            else if (currentElement != enemyMagicType)
            {
                damageReceived = Mathf.FloorToInt(damageAmount * MagicDamage);

                CriticalDamage(damageReceived);
                enemyScript.TakeDamage(amountForCalc);
                elementForCalc = true;
            }
            else if ((currentElement == MagicType.Fire && enemyMagicType == MagicType.Fire) || (currentElement == MagicType.Ice && enemyMagicType == MagicType.Ice))
            {
                damageReceived = 0;
                amountForCalc = 0;
                elementForCalc = false;
                isCritical = false;
            }
            
        }
    }
    
    public void CriticalDamage(int damageReceived)
    {
        float randomFloat = Random.Range(0f, 1f);
        isCritical = false;

        if (randomFloat < ChanceCriticalDamage)
        {
            isCritical = true;
            damageReceived *= 2;
        }

        amountForCalc = damageReceived;
    }
}




