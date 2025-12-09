using UnityEngine;

public class MainMenuBG : MonoBehaviour
{
    Camera MainMenuCamera;
    Color[] colors = new Color[]
    {
        new Color(0.25f, 0, 0),
        new Color(0.25f, 0.25f, 0),
        new Color(0, 0.25f, 0),
        new Color(0, 0.25f, 0.25f),
        new Color(0, 0, 0.25f),
        new Color(0.25f, 0, 0.25f),
    };
    int counter = 0;

    // Add a speed variable so you can change it in the Inspector
    [Range(0.1f, 5f)]
    public float changeSpeed = 1.0f;

    void Start()
    {
        MainMenuCamera = GetComponent<Camera>();
    }

    void Update()
    {
        Color targetColor = colors[counter];

        MainMenuCamera.backgroundColor = Color.Lerp(MainMenuCamera.backgroundColor, targetColor, Time.deltaTime * changeSpeed);

        if (Mathf.Abs(MainMenuCamera.backgroundColor.r - targetColor.r) < 0.05f &&
            Mathf.Abs(MainMenuCamera.backgroundColor.g - targetColor.g) < 0.05f &&
            Mathf.Abs(MainMenuCamera.backgroundColor.b - targetColor.b) < 0.05f)
        {
            counter++;
            counter = counter % colors.Length;
        }
    }
}
