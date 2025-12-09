using UnityEngine;

public class CelestialWatcher : Enemy
{
    System.Random Random = new System.Random();
    int AttackVariation = 0;
    public GameObject Lightning, Fireball, Arrow, PurpleSlime, GoblinScout, Bat;
    int LightningInit = 0, ArrowInit = 0, RandomArrow;
    float LightningCount = 0, ArrowCount = 0;
    bool ArrowLaunched = false;
    bool FireballLaunched = false;
    bool PurpleSlimeLaunched = false;
    bool GoblinScoutLaunched = false;
    bool BatLaunched = false;

    void Update()
    {
        if (AttackVariation == 0)
        {
            transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y), new Vector3(0, 2.5f), 6 * Time.deltaTime);
        }
        if (AttackVariation == 1)
        {
            transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y), new Vector3(Target.transform.position.x, Target.transform.position.y), 2 * Time.deltaTime);
        }
        if (AttackVariation == 2)
        {
            transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y), new Vector3(0, 20), 0.1f);
            if (LightningCount >= 30)
            {
                if (LightningInit < 9)
                {
                    GameObject Prefab = Instantiate(Lightning, new Vector3(-26 + (6.5f * LightningInit), 13), Quaternion.identity);
                    Prefab.GetComponent<Lightning>().Target = Target;
                    LightningInit++;
                }
                LightningCount = 0;
            }
            LightningCount += 1;
        }
        if (AttackVariation == 3)
        {
            transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y), new Vector3(0, 20), 0.1f);
            if (!FireballLaunched)
            {
                GameObject FireballPrefab = Instantiate(Fireball);
                FireballPrefab.GetComponent<Fireball>().Target = Target;
                FireballPrefab.transform.position = new Vector3(Target.transform.position.x, 30);
                FireballLaunched = true;
            }
        }
        if (AttackVariation == 4)
        {
            transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y), new Vector3(0, 20), 0.1f);
            if (!ArrowLaunched)
            {
                RandomArrow = Random.Next(8, 16);
                ArrowLaunched = true;
            }
            if (ArrowCount >= 50)
            {
                if (ArrowInit < RandomArrow)
                {
                    GameObject Prefab = Instantiate(Arrow, transform.position, Quaternion.identity);
                    Prefab.GetComponent<Projectile>().Target = Target;
                    ArrowInit++;
                }
                ArrowCount = 0;
            }
            ArrowCount += 1;
        }
        if (AttackVariation == 5)
        {
            transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y), new Vector3(0, 20), 0.1f);
            if (!PurpleSlimeLaunched)
            {
                int Count = Random.Next(3, 8);
                for (int a = 0; a < Count; a++)
                {
                    GameObject Prefab = Instantiate(PurpleSlime, transform.position, Quaternion.identity);
                    Prefab.GetComponent<Enemy>().Target = Target;
                }
                PurpleSlimeLaunched = true;
            }
        }
        if (AttackVariation == 6)
        {
            transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y), new Vector3(0, 20), 0.1f);
            if (!GoblinScoutLaunched)
            {
                int Count = Random.Next(3, 10);
                for (int a = 0; a < Count; a++)
                {
                    GameObject Prefab = Instantiate(GoblinScout, transform.position, Quaternion.identity);
                    Prefab.GetComponent<Enemy>().Target = Target;
                }
                GoblinScoutLaunched = true;
            }
        }
        if (AttackVariation == 7)
        {
            transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y), new Vector3(0, 20), 0.1f);
            if (!BatLaunched)
            {
                int Count = Random.Next(3, 8);
                for (int a = 0; a < Count; a++)
                {
                    GameObject Prefab = Instantiate(Bat, transform.position, Quaternion.identity);
                    Prefab.GetComponent<Enemy>().Target = Target;
                }
                BatLaunched = true;
            }
        }
        AttackTimer -= Time.deltaTime;
        if (AttackTimer <= 0)
        {
            LightningInit = 0;
            LightningCount = 0;
            ArrowInit = 0;
            ArrowCount = 0;
            ArrowLaunched = false;
            FireballLaunched = false;
            PurpleSlimeLaunched = false;
            GoblinScoutLaunched = false;
            BatLaunched = false;
            if (AttackVariation == 0)
            {
                while (AttackVariation == 0)
                {
                    AttackVariation = Random.Next(0, 8);
                }
            }
            else if (AttackVariation == 1)
            {
                while (AttackVariation == 1)
                {
                    AttackVariation = Random.Next(0, 8);
                }
            }
            else
            {
                AttackVariation = Random.Next(0, 8);
            }
            AttackTimer = AttackCD;
            if (AttackCD > 2.5f)
            {
                AttackCD -= 0.1f;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            RangeCD = MaxRangeCD;
            Rigidbody2D.AddForce(new Vector2(FacingLeft ? 10 : -10, 10), ForceMode2D.Impulse);
            if (CurrentHealth - Target.GetComponent<Entity>().Attack <= 0)
            {
                GlobalValues.CelestialWatcherDefeated = true;
                DialogManager.AddDialog("Celestial Watcher: I see.");
                DialogManager.AddDialog("Celestial Watcher: You truly are a hero.");
                DialogManager.ShowDialog();
            }
            TakeDamage(Target.GetComponent<Entity>().Attack);
        }
        if (collision.CompareTag("Player"))
        {
            Target.GetComponent<Entity>().TakeDamage(Attack);
        }
    }
}
