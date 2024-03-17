using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] GameObject Game;
    [SerializeField] GameObject Menu;

    //начало игры
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    //выход из игры
    public void ExitGame()
    {
        Application.Quit();
    }
}
