using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public string Name;
    public GameObject Player;

    public virtual void Interact()
    {
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player = collision.gameObject;
            GlobalValues.PlayerInteractableObject = this;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player = null;
            GlobalValues.PlayerInteractableObject = null;
        }
    }
}
