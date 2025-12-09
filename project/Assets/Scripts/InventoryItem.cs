using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public string Name;
    public Sprite Icon;
    public string Effect; // Health, Max Health, Damage Boost
    public string EffectAmount;

    public InventoryItem(string name, Sprite icon, string effect, string effectAmount)
    {
        Name = name;
        Icon = icon;
        Effect = effect;
        EffectAmount = effectAmount;
    }
}
