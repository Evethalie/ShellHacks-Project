using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.UI;
public enum MathType
{
    Addition,
    Subtraction,
    Multiplication,
    Division,
}
public class QuestionManager : MonoBehaviour
{


    public Text questionText;
    public InputField answerInput;
    public Text feedback;

    [SerializeField] private int correctAnswers = 0;
    [SerializeField] private int incorrectAnswers = 0;
    [SerializeField] private int currentQuestionDifficulty = 1;

    [SerializeField] private string currentQuestion;
    [SerializeField] private int currentAnswer;

    public MathType[] mathChoice;
    [SerializeField] MathType currentChoice;

    public Canvas canvas;
    public Dropdown choices;

    int number1 = 0;
    int number2 = 0;
    

    private void Awake()
    {
        canvas.GetComponent<Canvas>().enabled = true;
        mathChoice = (MathType[])System.Enum.GetValues(typeof(MathType));
        
    }
    void Start()
    {
        GenerateNewQuestion(choices.value);
    }

    public void SubmitAnswer()
    {
        int playerAnswer = int.Parse(answerInput.text);

        if (playerAnswer == currentAnswer)
        {
            feedback.text = "Correct!";
            correctAnswers++;
            incorrectAnswers = 0;
            AdjustDifficulty();
            Player player = FindObjectOfType<Player>();
            Enemy enemy = FindObjectOfType<Enemy>();
            player.DealDamage(enemy);

           
        }
        else
        {
            feedback.text = "Incorrect!";
            incorrectAnswers++;
            correctAnswers = 0;
            AdjustDifficulty();
            Player player = FindObjectOfType<Player>();
            Enemy enemy = FindObjectOfType<Enemy>();
            enemy.DealDamage(player);
        }
        answerInput.text = "";
        GenerateNewQuestion(choices.value);
    }

    void GenerateNewQuestion(int choice)
    {
        switch (choice)
        {
            case 0:
                number1 = Random.Range(1, 10 * currentQuestionDifficulty);
                number2 = Random.Range(1, 10 * currentQuestionDifficulty);
                currentAnswer = number1 + number2;
                currentQuestion = "" + number1 + " + " + number2 + "?";
                questionText.text = currentQuestion;
                break;
            case 1:
                number1 = Random.Range(10 * currentQuestionDifficulty, 1);
                number2 = Random.Range(1, 10 * currentQuestionDifficulty);
                currentAnswer = number1 - number2;
                currentQuestion = "" + number1 + " - " + number2 + "?";
                questionText.text = currentQuestion;
                break;
            case 2:
                number1 = Random.Range(1, 10 * currentQuestionDifficulty);
                number2 = Random.Range(1, 10 * currentQuestionDifficulty);
                currentAnswer = number1 * number2;
                currentQuestion = "" + number1 + " x " + number2 + "?";
                questionText.text = currentQuestion;
                break;
            case 3:
                number1 = Random.Range(10 * currentQuestionDifficulty, 1);
                number2 = Random.Range(1, 10 * currentQuestionDifficulty);
                currentAnswer = number1 / number2;
                currentQuestion = "" + number1 + " + " + number2 + "?";
                questionText.text = currentQuestion;
                break;
            default:
                Debug.Log("Error");
                return;

        
        }
    }

    void AdjustDifficulty()
    {
        if (correctAnswers >= 3)
        {
            currentQuestionDifficulty++;
            feedback.text += " Difficulty increased!";
        }
        else if (incorrectAnswers >= 3)
        {
            currentQuestionDifficulty = Mathf.Max(1, currentQuestionDifficulty - 1);
            feedback.text += " Difficulty decreased!";
        }
    }
    // Update is called once per frame
    public int Choice(MathType mathChoice)
    {
        switch(mathChoice)
        {
            case (MathType)0:
                return 0;
            case (MathType)1:
                return 1;

            case (MathType)2:
                return 2;
            case (MathType)3:
                return 3;
            default:
                return 4;

        }
    }
    public void closeCanvas()
    {
        canvas.GetComponent<Canvas>().enabled = false;
    }
}
