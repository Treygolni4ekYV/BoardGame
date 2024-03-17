using System;
using UnityEngine;

using Random = System.Random;

public class PlayerChipScript : MonoBehaviour
{
    [SerializeField] GameObject PointsContainer; //���������� ���� ��������� � ������� �� �����
    [SerializeField] int CurrentPointNumber = 1; //��������� ������� ������
    [SerializeField] internal int MovedToPointNumber = 1; //���� ���� �����
    [SerializeField] float MoveSpeed = 10f; //�������� ������
    [SerializeField] GameController gameController;
    [SerializeField] public bool isSelected = false;

    Transform[] points; //������ ��� �����

    Vector3 moveTo;
    Vector3 moveToFinal;


    private void Start()
    {
        //������ �����
        PointsContainer = GameObject.Find("PointsContainer");
        points = PointsContainer.GetComponentsInChildren<Transform>(false);
        //������ ���������� ����
        gameController = GameObject.Find("Game").GetComponent<GameController>();
    }

    private void FixedUpdate()
    {
        //�������� �� ���������� ������
        if (isSelected)
        {
            //�������� �� ���������� ������   
            if ((CurrentPointNumber < MovedToPointNumber || CurrentPointNumber != MovedToPointNumber) && transform.position != moveToFinal)
            {
                //������������ ��������(����� �� �������)
                if (MovedToPointNumber >= points.Length)
                {
                    MovedToPointNumber = 1;
                }
                if (CurrentPointNumber >= points.Length)
                {
                    CurrentPointNumber = 1;
                }

                //�������� �� ����� ���� � ��� �����
                if (CurrentPointNumber+1 != points.Length)
                {
                    //�������� ��������� � ����������� �� � ��������� �����
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
