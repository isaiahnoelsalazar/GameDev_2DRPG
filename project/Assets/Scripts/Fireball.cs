using UnityEngine;

public class Fireball : MonoBehaviour
{
    Vector3 TargetLastPosition;
    public GameObject Target;
    public float ProjectileSpeed, Damage;

    void Start()
    {
        TargetLastPosition = GlobalValues.GlobalPlayerInstance.transform.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, TargetLastPosition.y), ProjectileSpeed);
        if (transform.position.y == TargetLastPosition.y)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Target.GetComponent<Entity>().TakeDamage(Damage);
            Destroy(gameObject);
        }
    }
}
