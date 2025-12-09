using UnityEngine;

public class Lightning : MonoBehaviour
{
    public GameObject Target;
    public float Damage = 5f, AttackTimer = 1f;

    void Update()
    {
        AttackTimer -= Time.deltaTime;
        if (AttackTimer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Target.GetComponent<Entity>().TakeDamage(Damage);
        }
    }
}
