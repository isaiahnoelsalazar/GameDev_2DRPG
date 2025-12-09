using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField]
    GameObject DialogPanel;
    [SerializeField]
    Text DialogMessage;

    void Start()
    {
        GlobalValues.GlobalDialogPanel = DialogPanel;
        GlobalValues.GlobalDialogMessage = DialogMessage;
    }

    public static void AddDialog(string message)
    {
        GlobalValues.GlobalDialogMessageList.Add(message);
    }

    public static void ShowDialog()
    {
        GlobalValues.NoMove = true;
        GlobalValues.GlobalDialogMessage.text = GlobalValues.GlobalDialogMessageList[0];
        GlobalValues.GlobalDialogPanel.SetActive(true);
    }

    public void CloseDialog()
    {
        GlobalValues.GlobalDialogMessageList.RemoveAt(0);
        if (CelestialWatcherDialog.CelestialWatcherDialogActive)
        {
            CelestialWatcherDialog.CelestialWatcherDialogCounter++;
        }
        if (GlobalValues.GlobalDialogMessageList.Count <= 0)
        {
            if (CelestialWatcherDialog.CelestialWatcherDialogActive && CelestialWatcherDialog.CelestialWatcherDialogCounter == 3)
            {
                SceneManager.LoadScene(2);
                PlayerPrefs.SetInt("HasSavedStats", 0);
                PlayerPrefs.SetFloat("PlayerMaxHealth", GlobalValues.GlobalPlayerInstance.MaxHealth);
                PlayerPrefs.SetFloat("PlayerCurrentHealth", GlobalValues.GlobalPlayerInstance.CurrentHealth);
                PlayerPrefs.SetFloat("PlayerAttack", GlobalValues.GlobalPlayerInstance.Attack);
                PlayerPrefs.Save();
                CelestialWatcherDialog.CelestialWatcherDialogActive = false;
            }
            if (GlobalValues.FinalArea)
            {
                SceneManager.LoadScene(5);
                PlayerPrefs.SetInt("HasSavedStats", 0);
                PlayerPrefs.SetFloat("PlayerMaxHealth", GlobalValues.GlobalPlayerInstance.MaxHealth);
                PlayerPrefs.SetFloat("PlayerCurrentHealth", GlobalValues.GlobalPlayerInstance.CurrentHealth);
                PlayerPrefs.SetFloat("PlayerAttack", GlobalValues.GlobalPlayerInstance.Attack);
                PlayerPrefs.Save();
            }
            if (GlobalValues.CelestialWatcherDefeated)
            {
                SceneManager.LoadScene(6);
                PlayerPrefs.SetInt("HasSavedStats", 0);
                PlayerPrefs.SetFloat("PlayerMaxHealth", GlobalValues.GlobalPlayerInstance.MaxHealth);
                PlayerPrefs.SetFloat("PlayerCurrentHealth", GlobalValues.GlobalPlayerInstance.CurrentHealth);
                PlayerPrefs.SetFloat("PlayerAttack", GlobalValues.GlobalPlayerInstance.Attack);
                PlayerPrefs.Save();
            }
            GlobalValues.DialogOut = true;
        }
        else
        {
            GlobalValues.GlobalDialogMessage.text = GlobalValues.GlobalDialogMessageList[0];
        }
    }
}
