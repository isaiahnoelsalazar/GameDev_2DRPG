using UnityEngine;
using UnityEngine.UI;

public class SceneEntryFade : MonoBehaviour
{
    [SerializeField]
    Image Fade;
    float alpha = 0;
    [SerializeField]
    bool ShouldShowFirstDialog;
    [SerializeField]
    string[] ShowFirstDialogMessage;

    void Start()
    {
        alpha = Fade.color.a;
    }

    void Update()
    {
        if (alpha > 0)
        {
            alpha -= 0.02f;
            Fade.color = new Color(0, 0, 0, alpha);
        }
        else
        {
            if (ShouldShowFirstDialog)
            {
                foreach (string Dialog in ShowFirstDialogMessage)
                {
                    DialogManager.AddDialog(Dialog);
                }
                DialogManager.ShowDialog();
                ShouldShowFirstDialog = false;
            }
        }
    }
}
