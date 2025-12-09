using UnityEngine;

public class AreaEntryDialog : MonoBehaviour
{
    public string[] Dialogs;
    public int counter = 0;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
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
