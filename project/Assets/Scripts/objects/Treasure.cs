using UnityEngine;

public class Treasure : InteractableObject
{
    public string[] LootName, LootEffect;
    public string[] LootEffectAmount;
    public Sprite[] LootSpriteArray;

    void Start()
    {
        Name = "Treasure";
    }

    public override void Interact()
    {
        System.Random random = new System.Random();
        int Index = random.Next(LootName.Length);
        GlobalValues.GlobalPlayerInstance.AddToInventory(new InventoryItem(LootName[Index], LootSpriteArray[Index], LootEffect[Index], LootEffectAmount[Index]));
        Destroy(gameObject);
    }
}
