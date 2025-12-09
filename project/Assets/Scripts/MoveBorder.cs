using UnityEngine;

public class MoveBorder : AreaEntryDialog
{
    [SerializeField]
    GameObject Border;
    [SerializeField]
    float NewBorderX;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (counter <= 0)
            {
                GlobalValues.FinalArea = true;
                Border.transform.position = new Vector3(NewBorderX, Border.transform.position.y);
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
