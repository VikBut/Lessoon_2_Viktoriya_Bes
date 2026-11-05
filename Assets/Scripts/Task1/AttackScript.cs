using UnityEngine;

public class AttackScript : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))  // Старая Input-система, пока не буду на новую переходить 
        {
            animator.SetBool("attack", true);
            
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            animator.SetBool("attack", false);
        }
    }
}
