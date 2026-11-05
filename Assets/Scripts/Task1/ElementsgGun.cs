using System;
using Task1;
using UnityEngine;
using Random = UnityEngine.Random;

public class ElementsGun : MonoBehaviour
{
    private const float MagicDamage = 1.5f;
    private const float ChanceCriticalDamage = 0.3f;
    
    [SerializeField] 
    private Calculator m_calculator;
    
    private Material m_material;
    private Color m_colorA = Color.orange;
    private Color m_colorB = Color.blue;
    private Color m_defaultColor;
    private Color m_endColor;
    private float m_randomValue;
    
    [NonSerialized] 
    public bool isCritical;
    
    [NonSerialized] 
    public bool elementForCalc;
    
    [NonSerialized] 
    public int damageReceived;
    
    public MagicType currentElement;
    public int amountForCalc;
    public int damageAmount;
    
    public void Start()
    {
        m_material = GetComponent<Renderer>().material;
        m_defaultColor = m_material.color;
    }

    public Color ApplyColor(MagicType newElement)
    {
        switch (newElement)
        {
            case MagicType.Fire:
            {
                return m_colorA;
            }
            case MagicType.Ice:
            {
                return m_colorB;
            }
            case MagicType.None:
            {
                return m_defaultColor;
            }
        }

        return default;
    }

    private void OnValidate()
    {
        if (m_material != null && Application.isPlaying)
        {
            m_material.color = ApplyColor(currentElement);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        elementForCalc = false;

        if (other.CompareTag("Enemy"))
        {
            EnemyScript enemyScript = other.GetComponent<EnemyScript>();
            MagicType enemyMagicType = enemyScript.currentElementEnemy;
            damageReceived = Mathf.Max(1, damageAmount - enemyScript.MArmor);

            if (currentElement == MagicType.None || enemyMagicType == MagicType.None)
            {
                CriticalDamage(damageReceived);
                enemyScript.TakeDamage(amountForCalc);
            }
            else if (currentElement != enemyMagicType)
            {
                damageReceived = Mathf.FloorToInt(damageReceived * MagicDamage);

                CriticalDamage(damageReceived);
                enemyScript.TakeDamage(amountForCalc);
                elementForCalc = true;
            }
            else if ((currentElement == MagicType.Fire && enemyMagicType == MagicType.Fire)
                     || (currentElement == MagicType.Ice && enemyMagicType == MagicType.Ice))
            {
                damageReceived = 0;
                amountForCalc = 0;
                elementForCalc = false;
                isCritical = false;

                enemyScript.TakeDamage(0);
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




