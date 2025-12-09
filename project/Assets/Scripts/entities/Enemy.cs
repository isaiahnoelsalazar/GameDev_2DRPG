using UnityEngine;

public class Enemy : Entity
{
    public GameObject Target;

    public float MaxRangeCD = 1.5f;
    public float RangeCD;
    public float MinRange, MaxRange;

    public bool DelayedAttack;

    public int LootChance;
    public Treasure LootPrefab;
    public string[] LootName, LootEffect;
    public string[] LootEffectAmount;
    public Sprite[] LootSpriteArray;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            RangeCD = MaxRangeCD;
            Rigidbody2D.AddForce(new Vector2(FacingLeft ? 10 : -10, 10), ForceMode2D.Impulse);
            TakeDamage(Target.GetComponent<Entity>().Attack);
        }
    }

    public override void TakeDamage(float Amount)
    {
        base.TakeDamage(Amount);
        if (CurrentHealth <= 0)
        {
            GlobalValues.KilledEnemies++;
            System.Random random = new System.Random();
            if (random.Next(100) < LootChance)
            {
                LootPrefab.LootName = LootName;
                LootPrefab.LootSpriteArray = LootSpriteArray;
                LootPrefab.LootEffect = LootEffect;
                LootPrefab.LootEffectAmount = LootEffectAmount;
                Instantiate(LootPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}
