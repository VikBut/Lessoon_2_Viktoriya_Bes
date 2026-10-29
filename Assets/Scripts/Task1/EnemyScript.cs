using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyScript : MonoBehaviour
{
    [SerializeField, Range(0, 99)] private int armor;
    [SerializeField] private ElementsGun typeGun; 
    
    private Material material;
    private int HP = 100;
    public Animator animator;
    public Slider healthSlider;
    private float randomValue;
    private Color colorA = Color.orange;
    private Color colorB = Color.blue;
    private Color colorC;
    private Color defaultColorEnemy;
    public MagicType currentElementEnemy;

    void Start()
    {
        material = GetComponent<Renderer>().material;
        defaultColorEnemy = material.color;
        colorC = defaultColorEnemy;
    }
    void Update()
    {
        healthSlider.value = HP;
    }

    public Color ApplyColor()
    {
        switch (currentElementEnemy)
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
                return colorC;
            }
        }

        return default;
    }

    private void OnValidate()
    {
        if (material != null && Application.isPlaying)
        {
            defaultColorEnemy = ApplyColor();
            material.color = defaultColorEnemy;
        }
    }
    
    public void StartEffect()
    {
        StartCoroutine(ColorDamage());
    }

    private IEnumerator ColorDamage()
    {
        material.color = Color.red;

        yield return new WaitForSeconds(0.5f);

        material.color = defaultColorEnemy;
    }
    
    public int lastDamage;
    
    public void TakeDamage(int damage)
    {
            lastDamage = Mathf.Clamp(damage - armor, 1, 100);
            HP -= lastDamage;
            

            
        
        
        if (HP <= 0)
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
