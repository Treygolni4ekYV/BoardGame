using System;
using UnityEngine;
using TMPro;
using Random = System.Random;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    [SerializeField] int SelectedPlayerID = 0;//id выбранного игрока
    [SerializeField] public GameObject[] Players;// игроки *устанавливается из скрипта по их созданию[PlayerSelectorScript.cs]
    [SerializeField] GameObject PointsContainer; //если честно, пока не ебу
    [SerializeField] public bool[] PlayersInGame; //значение, в игре ли игроки [fasle - игрок прошел/выбыл], содержит столько элементов, сколько и игроков в игре

    [SerializeField] string[] PlayersNames;
    List<string> winners = new List<string>();

    [SerializeField] GameObject GameUI;
    [SerializeField] GameObject TestsUI;
    [SerializeField] GameObject Dice;
    [SerializeField] CameraScript Camera_CameraScript;
    [SerializeField] GameObject SelectedPlayerName;
    [SerializeField] GameObject GameEndScreen;

    [SerializeField] GameObject OkButton;
    [SerializeField] GameObject DropButton;
    [SerializeField] GameObject DiceImage;

    public void Start()
    {
        TestsUI.GetComponent<TestScript>().LoadData();
    }

    //начало хода игрока
    public void MakeMove()
    {
        TestsUI.SetActive(true);
        TestsUI.GetComponent<TestScript>().StartTest();
    }

    //начало передвижения игрока
    public void StartMoving()
    {
        DropButton.SetActive(true);
        OkButton.SetActive(false);

        DiceImage.SetActive(false);
        Dice.SetActive(false);
        GameUI.SetActive(true);
        Players[SelectedPlayerID].GetComponent<PlayerChipScript>().GoToPoint(DiceScript.RandomValue);
        Camera_CameraScript.goTo = Players[SelectedPlayerID];
    }

    //переключение на следующего игрока
    public void NextPlayer()
    {
        bool playerInGameConsist = false;

        //проверка, на то, есть ли ещё игроки
        foreach (var player in PlayersInGame) 
        {
            if (player == true)
            {
                playerInGameConsist = true;
                break;
            }
        }

        if (playerInGameConsist)
        {
            int currentIndex = Array.IndexOf(PlayersInGame, true, SelectedPlayerID+1);
            if (currentIndex == -1)
            {
                currentIndex = Array.IndexOf(PlayersInGame, true);
            }
            SelectedPlayerID = currentIndex;

            Camera_CameraScript.goTo = Players[SelectedPlayerID];
            SelectedPlayerName.GetComponent<TMP_Text>().text = PlayersNames[SelectedPlayerID];
        }
        else
        {
            GameEndScreen.SetActive(true);
            GameEndScreen.GetComponent<GameEndScript>().EndGame(winners);
        }


    }

    public void SetPlayersNames(string[] names)
    {
        PlayersNames = names;
        SelectedPlayerName.GetComponent<TMP_Text>().text = PlayersNames[SelectedPlayerID];
    }

    public void PlayerEnd()
    {
        winners.Add(PlayersNames[SelectedPlayerID]);
        PlayersInGame[SelectedPlayerID] = false;
    }

}
