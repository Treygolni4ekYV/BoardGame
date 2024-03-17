using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] GameObject Game;
    [SerializeField] GameObject Menu;

    //������ ����
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    //����� �� ����
    public void ExitGame()
    {
        Application.Quit();
    }
}
