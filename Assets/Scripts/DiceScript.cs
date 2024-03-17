using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class DiceScript : MonoBehaviour
{
    [SerializeField] GameObject DiceImage;
    [SerializeField] Sprite[] DiceImages;

    [SerializeField] GameObject DiceGoButton;
    [SerializeField] GameObject DiceOkButton;

    public static int RandomValue = 1;

    public void GetRandomValue()
    {
        Random random = new Random();
        RandomValue = random.Next(1, 7);

        DiceGoButton.SetActive(false);

        DiceImage.SetActive(true);
        StartCoroutine(ChangeImageWithDelay());
    }

    int changeCount = 0;
    int numberOfChanges = 4;
    float delaySeconds = 0.5f;

    IEnumerator ChangeImageWithDelay()
    {
        while (changeCount < numberOfChanges)
        {
            // Изменяем изображение на следующее в массиве
            DiceImage.GetComponent<Image>().sprite = DiceImages[new Random().Next(1, 7)-1];
            changeCount++;

            // Ждем заданное количество секунд перед следующим изменением
            yield return new WaitForSeconds(delaySeconds - 0.075f*changeCount);
        }
        DiceImage.GetComponent<Image>().sprite = DiceImages[RandomValue - 1];
        DiceOkButton.SetActive(true);
        changeCount = 0;
    }
}
