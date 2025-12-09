using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    GameObject AboutPanel, HowToPlayPanel;

    public void CloseHowToPlayPanel()
    {
        HowToPlayPanel.SetActive(false);
    }

    public void CloseAboutPanel()
    {
        AboutPanel.SetActive(false);
    }

    public void Button_Start()
    {
        PlayerPrefs.SetInt("HasSavedStats", -1);
        PlayerPrefs.Save();
        SceneManager.LoadScene(1);
    }

    public void Button_HowToPlay()
    {
        HowToPlayPanel.SetActive(true);
    }

    public void Button_About()
    {
        AboutPanel.SetActive(true);
    }

    public void Button_Quit()
    {
        Application.Quit();
    }
}
