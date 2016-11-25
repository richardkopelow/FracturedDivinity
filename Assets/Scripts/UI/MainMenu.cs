using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject HasSaveSet;
    public GameObject NoSaveSet;

    public void Start()
    {
        if (PlayerPrefs.GetInt("SeenCutscene")==1)
        {
            HasSaveSet.SetActive(true);
            NoSaveSet.SetActive(false);
        }
    }

    public void OnExitClicked()
    {
        Application.Quit();
    }

    public void OnNewGameClickedClicked()
    {
        SceneManager.LoadScene("IntroCutscene");
    }
    public void OnContinueClicked()
    {
        SceneManager.LoadScene("HomeBase");
    }
    public void OnClearSaveClicked()
    {
        PlayerPrefs.DeleteAll();
        HasSaveSet.SetActive(false);
        NoSaveSet.SetActive(true);
    }
}
