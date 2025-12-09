using UnityEngine;

public class Area : MonoBehaviour
{
    [SerializeField]
    Camera Camera;

    bool Triggered = false;

    public float NewPlayerX, NewCameraX;
    public bool ShouldReplaceMinCameraRange;
    public int MinCameraRange;

    void Update()
    {
        if (Triggered && GlobalValues.OngoingDirection == this)
        {
            if (Camera.transform.position != new Vector3(NewCameraX, 4, -10))
            {
                Camera.transform.position = Vector3.Lerp(Camera.transform.position, new Vector3(NewCameraX, 4, -10), 0.1f);
            }
            else
            {
                Triggered = false;
            }
        }
        else
        {
            Triggered = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GlobalValues.OngoingDirection = this;
            Triggered = true;
            collision.gameObject.transform.position = new Vector2(NewPlayerX, collision.gameObject.transform.position.y);
            collision.gameObject.GetComponent<Player>().Dashing = false;
            if (ShouldReplaceMinCameraRange)
            {
                Camera.GetComponent<CameraMovement>().MinRange = MinCameraRange;
            }
            Camera.GetComponent<CameraMovement>().Target = null;
        }
    }
}
