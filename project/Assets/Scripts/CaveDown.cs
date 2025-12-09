using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CaveDown : InteractableObject
{
    [SerializeField]
    Image Fade;

    float alpha = 0;
    bool faded = false;
    bool Interacted = false;

    void Update()
    {
        if (Interacted)
        {
            GlobalValues.NoMove = true;
            if (!faded)
            {
                if (alpha < 1)
                {
                    alpha += 0.05f;
                    Fade.color = new Color(0, 0, 0, alpha);
                }
                else
                {
                    GlobalValues.PlayerInteractableObject = null;
                    GlobalValues.NoMove = false;
                    SceneManager.LoadScene(4);
                    faded = true;
                    PlayerPrefs.SetInt("HasSavedStats", 0);
                    PlayerPrefs.SetFloat("PlayerMaxHealth", GlobalValues.GlobalPlayerInstance.MaxHealth);
                    PlayerPrefs.SetFloat("PlayerCurrentHealth", GlobalValues.GlobalPlayerInstance.CurrentHealth);
                    PlayerPrefs.SetFloat("PlayerAttack", GlobalValues.GlobalPlayerInstance.Attack);
                    PlayerPrefs.Save();
                }
            }
        }
    }

    public override void Interact()
    {
        Interacted = true;
        GlobalValues.OngoingDirection = null;
        alpha = Fade.color.a;
    }
}
