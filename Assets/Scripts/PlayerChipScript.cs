using System;
using UnityEngine;

using Random = System.Random;

public class PlayerChipScript : MonoBehaviour
{
    [SerializeField] GameObject PointsContainer; //Подключаем сюда контейнер с точками на карте
    [SerializeField] int CurrentPointNumber = 1; //Настоящая позиция игрока
    [SerializeField] internal int MovedToPointNumber = 1; //Куда идет игрок
    [SerializeField] float MoveSpeed = 10f; //Скорость ходьбы
    [SerializeField] GameController gameController;
    [SerializeField] public bool isSelected = false;

    Transform[] points; //Массив для точек

    Vector3 moveTo;
    Vector3 moveToFinal;


    private void Start()
    {
        //Пиздим точки
        PointsContainer = GameObject.Find("PointsContainer");
        points = PointsContainer.GetComponentsInChildren<Transform>(false);
        //Пиздим контроллер игры
        gameController = GameObject.Find("Game").GetComponent<GameController>();
    }

    private void FixedUpdate()
    {
        //проверка на надобность сдвига
        if (isSelected)
        {
            //Проверка на надобность сдвига   
            if ((CurrentPointNumber < MovedToPointNumber || CurrentPointNumber != MovedToPointNumber) && transform.position != moveToFinal)
            {
                //Нормализация значений(лучше не трогать)
                if (MovedToPointNumber >= points.Length)
                {
                    MovedToPointNumber = 1;
                }
                if (CurrentPointNumber >= points.Length)
                {
                    CurrentPointNumber = 1;
                }

                //Проверка на конец пути и его сброс
                if (CurrentPointNumber+1 != points.Length)
                {
                    //Проверка координат и перемещение на к следующей точке
                    if (transform.position == moveTo)
                    {
                        CurrentPointNumber++;
                        if (CurrentPointNumber + 1 == MovedToPointNumber)
                        {
                            moveTo = moveToFinal;
                        }
                        else
                        {
                            MoveToRandom(CurrentPointNumber + 1, ref moveTo);
                        }
                    }
                    else
                    {
                        transform.position = Vector3.MoveTowards(transform.position, moveTo, MoveSpeed * Time.deltaTime);
                    }
                }
            }
            else
            {
                if (CurrentPointNumber+2 >= points.Length)
                {
                    gameController.PlayerEnd();
                    gameObject.SetActive(false);
                }
                gameController.NextPlayer();
                isSelected = false;
            }
        }
    }

    public void GoToPoint(int MoveOnPoint)
    {
        this.MovedToPointNumber += MoveOnPoint;
        if (this.MovedToPointNumber >= points.Length)
        {
            MovedToPointNumber = points.Length - 1;
        }

        MoveToRandom(MovedToPointNumber, ref moveToFinal);
        isSelected = true; 
    }

    public void MoveToRandom(int pointNumber, ref Vector3 outPosition)
    {
        float shiftY = Convert.ToSingle(new Random().NextDouble()) - 0.35f;
        float shiftX = Convert.ToSingle(new Random().NextDouble()) - 0.35f;
        Vector3 pointPosition = points[pointNumber].position;
        outPosition = new Vector3(pointPosition.x + shiftX, pointPosition.y + shiftY, pointPosition.z);
    }
}
