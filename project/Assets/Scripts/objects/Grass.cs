using UnityEngine;

public class Grass : InteractableObject
{
    void Start()
    {
        Name = "Grass";
    }

    public override void Interact()
    {
        GlobalValues.GlobalPlayerInstance.AddToInventory(new InventoryItem(Name, GetComponent<SpriteRenderer>().sprite, "Health", "2"));
        Destroy(gameObject);
    }
}
