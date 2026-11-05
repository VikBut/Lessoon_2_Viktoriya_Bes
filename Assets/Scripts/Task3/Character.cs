using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] 
    protected Animator animator;
    
    protected Material material;
    protected int healthPoint = 100;
}