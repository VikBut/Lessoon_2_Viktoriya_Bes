using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Character : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    protected Material material;
    protected int healthPoint = 100;
    protected float randomValue;
    protected Color defaultColor;
    protected Color colorC;
}