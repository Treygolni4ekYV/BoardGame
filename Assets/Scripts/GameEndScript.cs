using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndScript : MonoBehaviour
{
    [SerializeField] GameObject[] NamesLabels;
    
    public void EndGame(List<string> winners)
    {
        NamesLabels[0].GetComponent<TMP_Text>().text = winners[0];
        NamesLabels[1].GetComponent<TMP_Text>().text = winners[1];
        if (winners.Count >= 3) { NamesLabels[2].GetComponent<TMP_Text>().text = winners[2]; }
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}
