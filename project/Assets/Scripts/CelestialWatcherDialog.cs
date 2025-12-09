using UnityEngine;
using UnityEngine.UI;

public class CelestialWatcherDialog : MonoBehaviour
{
    public static bool CelestialWatcherDialogActive = false;
    public static int CelestialWatcherDialogCounter = 0;
    [SerializeField]
    Image Fade;
    float alpha = 0;

    void OnEnable()
    {
        CelestialWatcherDialogActive = true;
        DialogManager.AddDialog("A giant eye with wings suddenly appears in front of you.");
        DialogManager.AddDialog("It looks at you silently.");
        DialogManager.AddDialog("You suddenly feel a little lightheaded.");
        DialogManager.ShowDialog();
    }

    void Update()
    {
        if (CelestialWatcherDialogActive && CelestialWatcherDialogCounter == 2)
        {
            if (alpha < 1)
            {
                alpha += 0.05f;
                Fade.color = new Color(0, 0, 0, alpha);
            }
        }
    }
}
