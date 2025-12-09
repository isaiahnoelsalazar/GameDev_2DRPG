using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    InventoryItem Details;
    GameObject InventoryItemPanel;
    Text InventoryItemMessage;

    public void SetInventoryItemPanel(GameObject InventoryItemPanel)
    {
        this.InventoryItemPanel = InventoryItemPanel;
    }

    public void SetInventoryItemMessage(Text InventoryItemMessage)
    {
        this.InventoryItemMessage = InventoryItemMessage;
    }

    public void SetInventoryItemMessageText()
    {
        string Total = string.Empty;
        for (int a = 0; a < Details.Effect.Split(',').Length; a++)
        {
            Total += $"+{Details.EffectAmount.Split(',')[a]} {Details.Effect.Split(',')[a]}, ";
        }
        Total = Total.TrimEnd(',', ' ');
        InventoryItemMessage.text = $"{Details.Name}\n{Total}";
    }

    public void SetInventoryItem(InventoryItem Details)
    {
        this.Details = Details;
    }

    public void DestroyItem()
    {
        for (int a = 0; a < GlobalValues.Inventory.Count; a++)
        {
            if (GlobalValues.Inventory[a].Name == Details.Name)
            {
                GlobalValues.Inventory.RemoveAt(a);
                InventoryItemPanel.SetActive(false);
                break;
            }
        }
        GlobalValues.GlobalPlayerInstance.RefreshInventory();
    }

    public void SlotClick()
    {
        string[] EffectSplit = Details.Effect.Split(',');
        string[] EffectAmountSplit = Details.EffectAmount.Split(',');
        for (int a = 0; a < EffectSplit.Length; a++)
        {
            if (EffectSplit[a] == "Health")
            {
                if ((GlobalValues.GlobalPlayerInstance.CurrentHealth + float.Parse(EffectAmountSplit[a])) < GlobalValues.GlobalPlayerInstance.MaxHealth)
                {
                    GlobalValues.GlobalPlayerInstance.CurrentHealth += float.Parse(EffectAmountSplit[a]);
                }
                else
                {
                    GlobalValues.GlobalPlayerInstance.CurrentHealth = GlobalValues.GlobalPlayerInstance.MaxHealth;
                }
                GlobalValues.GlobalPlayerInstance.RefreshHealth();
            }
            if (EffectSplit[a] == "Max Health")
            {
                GlobalValues.GlobalPlayerInstance.MaxHealth += float.Parse(EffectAmountSplit[a]);
                GlobalValues.GlobalPlayerInstance.RefreshHealth();
            }
            if (EffectSplit[a] == "Damage Boost")
            {
                GlobalValues.GlobalPlayerInstance.Attack += float.Parse(EffectAmountSplit[a]);
            }
        }
        DestroyItem();
    }

    public void MouseEnter()
    {
        InventoryItemPanel.SetActive(true);
        SetInventoryItemMessageText();
    }

    public void MouseExit()
    {
        InventoryItemPanel.SetActive(false);
    }
}
