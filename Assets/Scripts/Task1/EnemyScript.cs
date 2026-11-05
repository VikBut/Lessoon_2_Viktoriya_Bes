using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Task1
{
    public class EnemyScript : MonoBehaviour
    {
        [SerializeField]
        [Range(0, 99)]
        private int m_armor;
        
        private Color m_colorFire = Color.orange;
        private Color m_colorIce = Color.blue;
        private Color m_colorBase;
        private Color m_defaultColorEnemy;
        private Material m_material;
        private int m_hp = 100;
        private float m_randomValue;
        
        public MagicType currentElementEnemy;
        public Animator animator;
        public Slider healthSlider;
        public int MArmor => m_armor;

        void Start()
        {
            m_material = GetComponent<Renderer>().material;
            m_defaultColorEnemy = m_material.color;
            m_colorBase = m_defaultColorEnemy;
        }
        void Update()
        {
            healthSlider.value = m_hp;
        }

        public Color ApplyColor()
        {
            switch (currentElementEnemy)
            {
                case MagicType.Fire:
                {
                    return m_colorFire;
                }
                case MagicType.Ice:
                {
                    return m_colorIce;
                }
                case MagicType.None:
                {
                    return m_colorBase;
                }
            }

            return default;
        }

        private void OnValidate()
        {
            if (m_material != null && Application.isPlaying)
            {
                m_defaultColorEnemy = ApplyColor();
                m_material.color = m_defaultColorEnemy;
            }
        }
    
        public void StartEffect()
        {
            StartCoroutine(ColorDamage());
        }

        private IEnumerator ColorDamage()
        {
            m_material.color = Color.red;

            yield return new WaitForSeconds(0.5f);

            m_material.color = m_defaultColorEnemy;
        }
    
        public int lastDamage;
    
        public void TakeDamage(int damage)
        {
            lastDamage = Mathf.Clamp(damage, 0, 100);
            m_hp -= lastDamage;

            if (m_hp <= 0)
            {
                animator.SetTrigger("death");
                healthSlider.gameObject.SetActive(false);
            }
            else 
            {
                animator.SetTrigger("damage");
                StartEffect();
            
            }
        }
    }
}
