using UnityEngine;
using UnityEngine.UI;

public class Door : InteractableObject
{
    [SerializeField]
    Image Fade;
    [SerializeField]
    Camera Camera;

    float alpha = 0;
    bool faded = false;
    bool Interacted = false;

    public float NewPlayerX, NewCameraX, NewCameraSize, NewCameraY;
    public bool ShouldChangeCameraSize;

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
                    Player.transform.position = new Vector2(NewPlayerX, Player.transform.position.y);
                    Camera.transform.position = new Vector3(NewCameraX, 4, -10);
                    if (ShouldChangeCameraSize)
                    {
                        Camera.orthographicSize = NewCameraSize;
                        Camera.transform.position = new Vector3(NewCameraX, NewCameraY, -10);
                    }
                    faded = true;
                }
            }
            else
            {
                if (alpha > 0)
                {
                    alpha -= 0.05f;
                    Fade.color = new Color(0, 0, 0, alpha);
                }
                else
                {
                    GlobalValues.NoMove = false;
                    faded = false;
                    Interacted = false;
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
