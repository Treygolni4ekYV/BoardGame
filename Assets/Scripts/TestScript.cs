using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = System.Random;


public class TestScript : MonoBehaviour
{
    [SerializeField] TextAsset QuestionsJson;//JOSN � ���������

    [SerializeField] GameObject QuestionField;//��������� ������� 
    [SerializeField] GameObject[] AnswerFields;//�������� ������

    [SerializeField] GameObject Dice;//�� ���

    [SerializeField] GameController gameController;//������ ����

    Questions questionsContainer;//���������(�� ��� ������ �� � �����, ������ ��� �����
    int questionNumber;
    int correctAnswer;

    public void LoadData()
    {
        //��������� ������ � json-� ��� ������ ����
        questionsContainer = JsonUtility.FromJson<Questions>(QuestionsJson.text);
        Debug.Log(questionsContainer.questions.Count);
    }

    public void StartTest()
    {
        questionNumber = new Random().Next(0, questionsContainer.questions.Count);

        QuestionField.GetComponent<TMP_Text>().text = questionsContainer.questions[questionNumber].text;
        AnswerFields[0].GetComponent<TMP_Text>().text = questionsContainer.questions[questionNumber].answer0;
        AnswerFields[1].GetComponent<TMP_Text>().text = questionsContainer.questions[questionNumber].answer1;
        AnswerFields[2].GetComponent<TMP_Text>().text = questionsContainer.questions[questionNumber].answer2;
        AnswerFields[3].GetComponent<TMP_Text>().text = questionsContainer.questions[questionNumber].answer3;

        correctAnswer = questionsContainer.questions[questionNumber].correctAnswerIndex;
        Debug.Log(correctAnswer);
    }

    public void ConfirmChoice(int choiceAnswer) 
    {
        if (choiceAnswer == correctAnswer)
        {
            Debug.Log("Is Correct answer");
            Dice.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Is NOT Correct answer");
            gameController.NextPlayer();
            gameObject.SetActive(false);
        }
    }
}

[Serializable]
class Questions
{
    public List<Question> questions = new List<Question>();
}

[Serializable]
class Question
{
    public string text;
    public string answer0;
    public string answer1;
    public string answer2;
    public string answer3;
    public int correctAnswerIndex;
}