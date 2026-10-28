using UnityEngine;

public class AttackScript : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetBool("attack", true);
            float randomValue = Random.Range(0f, 1f);
            
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            animator.SetBool("attack", false);
        }
        
    }
}
