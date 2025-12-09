using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalValues
{
    public static bool FinalArea, NoMove, DialogOut;
    public static Area OngoingDirection;
    public static InteractableObject PlayerInteractableObject;
    public static GameObject GlobalDialogPanel;
    public static Player GlobalPlayerInstance;
    public static Text GlobalDialogMessage;
    public static List<string> GlobalDialogMessageList = new List<string>();
    public static List<InventoryItem> Inventory = new List<InventoryItem>();
    public static int KilledEnemies = 0;
    public static bool CelestialWatcherDefeated = false;

    //public static List<T> GetComponentsWithTag<T>(string tag, bool includeInactive = false) where T : Component
    //{
    //    List<T> results = new List<T>();

    //    GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tag);

    //    foreach (GameObject obj in taggedObjects)
    //    {
    //        if (!includeInactive && !obj.activeInHierarchy)
    //        {
    //            continue;
    //        }

    //        T component = obj.GetComponent<T>();
    //        if (component != null)
    //        {
    //            results.Add(component);
    //        }
    //    }

    //    return results;
    //}

    public static int CountItemInInventory(string Name)
    {
        int count = 0;
        for (int a = 0; a < Inventory.Count; a++)
        {
            if (Inventory[a].Name == Name)
            {
                count++;
            }
        }
        return count;
    }
}
