using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform Target;
    public int MinRange, MaxRange;

    void Update()
    {
        if (transform.position != Target.position && Target != null)
        {
            Vector3 TargetPosition = new Vector3(Target.position.x > MinRange ? Target.position.x < MaxRange ? Target.position.x : MaxRange : MinRange, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, TargetPosition, 0.1f);
        }
    }
}
