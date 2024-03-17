using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScropt : MonoBehaviour
{
    [SerializeField] GameObject PauseUI;

    public void GoPause()
    {
        PauseUI.SetActive(true);
    } 

    public void ExitPause()
    {
        PauseUI.SetActive(false);
    }

    public void GoMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
