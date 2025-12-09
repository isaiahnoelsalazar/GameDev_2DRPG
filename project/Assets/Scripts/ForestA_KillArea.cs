using UnityEngine;

public class ForestA_KillArea : AreaEntryDialog
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (GlobalValues.KilledEnemies == 4)
            {
                if (counter <= 0)
                {
                    foreach (string Dialog in Dialogs)
                    {
                        DialogManager.AddDialog(Dialog);
                    }
                    DialogManager.ShowDialog();
                    counter++;
                }
            }
        }
    }
}
