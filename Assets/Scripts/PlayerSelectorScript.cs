using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerSelectorScript : MonoBehaviour
{
    [SerializeField] GameObject[] NamesBoxes; //������� � ������� �������

    [SerializeField] GameObject PlayerPrefab; //������ ������
    [SerializeField] GameObject PlayerContainer; //��������� ��� �������

    [SerializeField] GameController gameController;
    [SerializeField] GameObject UI;

    


    //������ ����� ��� �������(��� 4-�)
    float[,] colors = new float[,]
    {
        {0.84f, 0f, 0f},
        {0.93f, 0.92f, 0.04f},
        {0f, 0.37f, 0.84f},
        {0f, 0.78f, 0.84f}
    };

    //��������� ���������� ����� ��� ��� �������� ��������
    public void SelectedPlayersCountChange(GameObject slider)
    {
        int sliderValue = Convert.ToInt32(slider.GetComponent<Slider>().value);
        for (int i = 0; i < sliderValue; i++)
        {
            NamesBoxes[i].SetActive(true);
        }
        if (sliderValue != NamesBoxes.Length)
        {
            for (int i = sliderValue; i < NamesBoxes.Length; i++)
            {
                NamesBoxes[i].SetActive(false);
            }
        }
    }

    //����� ������� ���������� ������� � ���������� �� ������
    public void ConfirmPlayersCount(GameObject slider)
    {
        int sliderValue = Convert.ToInt32(slider.GetComponent<Slider>().value);
        for(int i = 0;i <= sliderValue-1; i++)
        {
            Instantiate(PlayerPrefab, PlayerContainer.transform);
            PlayerContainer.GetComponentsInChildren<SpriteRenderer>(false)[i].color = new Color(colors[i,0],colors[i,1],colors[i,2]);
        }
        bool[] PlayersInGame = new bool[sliderValue];

        for (int i = 0; i < sliderValue; i++)
        {
            PlayersInGame[i] = true;
        }

        gameController.PlayersInGame = PlayersInGame;

        GameObject[] Players = new GameObject[PlayerContainer.transform.childCount];
        for (int i = 0; i < Players.Length; i++)
        {
            Players[i] = PlayerContainer.transform.GetChild(i).gameObject;
        }
        gameController.Players = Players;

        //������� ���� �������
        string[] names = new string[sliderValue];
        for (int i = 0; i < names.Length; i++)
        {
            names[i] = NamesBoxes[i].GetComponent<TMP_InputField>().text;
        }
        gameController.SetPlayersNames(names);


        UI.SetActive(true);
        Destroy(gameObject);
    }

}
