using UnityEngine;

public class Enemy_MeleeFlying : Enemy
{
    bool TargetInRange;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        CurrentHealth = MaxHealth;
        AttackTimer = AttackCD;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, Target.transform.position) <= MaxRange && Vector3.Distance(transform.position, Target.transform.position) >= MinRange)
        {
            if (Target.transform.position.x < transform.position.x)
            {
                GetComponent<SpriteRenderer>().flipX = true;
                FacingLeft = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
                FacingLeft = false;
            }
            RangeCD -= Time.deltaTime;
            if (RangeCD <= 0)
            {
                transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y), new Vector3(Target.transform.position.x, Target.transform.position.y), Speed * Time.deltaTime);
                Animator.SetBool("IsMoving", true);
            }
        }
        else
        {
            RangeCD = MaxRangeCD;
            AttackTimer = AttackCD;
            Animator.SetBool("IsMoving", false);
        }
        if (TargetInRange)
        {
            AttackTimer -= Time.deltaTime;
            if (DelayedAttack)
            {
                if (AttackTimer > 0 && AttackTimer <= 0.5)
                {
                    Animator.SetBool("IsAttacking", true);
                }
                else if (AttackTimer <= 0)
                {
                    Animator.SetBool("IsAttacking", false);
                    Target.GetComponent<Entity>().TakeDamage(Attack);
                    AttackTimer = AttackCD;
                }
            }
            else
            {
                if (AttackTimer <= 0)
                {
                    Target.GetComponent<Entity>().TakeDamage(Attack);
                    AttackTimer = AttackCD;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            TakeDamage(Target.GetComponent<Entity>().Attack);
        }
        if (collision.CompareTag("Player"))
        {
            TargetInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TargetInRange = false;
        }
    }
}
