using UnityEngine;

public class CelestialWatcherArea : MonoBehaviour
{
    [SerializeField]
    GameObject CelestialWatcher;

    bool Triggered = false;

    void Update()
    {
        if (Triggered)
        {
            CelestialWatcher.transform.position = Vector3.Lerp(CelestialWatcher.transform.position, new Vector3(CelestialWatcher.transform.position.x, 8, 0), 0.1f);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (GlobalValues.CountItemInInventory("Grass") == 2)
            {
                CelestialWatcher.SetActive(true);
                GlobalValues.NoMove = true;
                Triggered = true;
            }
        }
    }
}
