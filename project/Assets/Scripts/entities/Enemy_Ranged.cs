using UnityEngine;

public class Enemy_Ranged : Enemy
{
    public GameObject ProjectilePrefab;
    float MaxProjectileRange = 10f;

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
                transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y), new Vector3(Target.transform.position.x, transform.position.y), Speed * Time.deltaTime);
                Animator.SetBool("IsMoving", true);
            }
        }
        else
        {
            RangeCD = MaxRangeCD;
            Animator.SetBool("IsMoving", false);
        }
        if (Vector3.Distance(transform.position, Target.transform.position) <= MaxProjectileRange)
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
                    GameObject Projectile = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);
                    Projectile.GetComponent<Projectile>().Target = Target;
                    Projectile.GetComponent<Projectile>().Damage = Attack;
                    AttackTimer = AttackCD;
                }
            }
            else
            {
                if (AttackTimer <= 0)
                {
                    GameObject Projectile = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);
                    Projectile.GetComponent<Projectile>().Target = Target;
                    Projectile.GetComponent<Projectile>().Damage = Attack;
                    AttackTimer = AttackCD;
                }
            }
        }
        else
        {
            Animator.SetBool("IsAttacking", false);
        }
    }
}
