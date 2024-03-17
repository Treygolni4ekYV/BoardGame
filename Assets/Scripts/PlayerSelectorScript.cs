using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerSelectorScript : MonoBehaviour
{
    [SerializeField] GameObject[] NamesBoxes; //объекты с Именами игроков

    [SerializeField] GameObject PlayerPrefab; //Префаб игрока
    [SerializeField] GameObject PlayerContainer; //Контейнер для игроков

    [SerializeField] GameController gameController;
    [SerializeField] GameObject UI;

    


    //Массив цетов для игроков(для 4-х)
    float[,] colors = new float[,]
    {
        {0.84f, 0f, 0f},
        {0.93f, 0.92f, 0.04f},
        {0f, 0.37f, 0.84f},
        {0f, 0.78f, 0.84f}
    };

    //Изменение количества полей для при движении слайдера
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

    //Спавн нужного количества игроков и применение их цветов
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

        //задание имен игроков
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
