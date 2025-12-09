using UnityEngine;

public class Entity : MonoBehaviour
{
    public float MaxHealth, CurrentHealth, Attack, Speed, AttackCD, AttackTimer;
    public bool FacingLeft;
    public Rigidbody2D Rigidbody2D;
    public Animator Animator;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        CurrentHealth = MaxHealth;
    }

    public virtual void TakeDamage(float Amount)
    {
        CurrentHealth -= Amount;
        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
