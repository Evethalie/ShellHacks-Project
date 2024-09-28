using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.UI;

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
    void Start()
    {
        GenerateNewQuestion();
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
            feedback.text = "Incorrect! The answer was " + currentAnswer + "!";
            incorrectAnswers++;
            correctAnswers = 0;
            AdjustDifficulty();
            Player player = FindObjectOfType<Player>();
            Enemy enemy = FindObjectOfType<Enemy>();
            enemy.DealDamage(player);
        }
        answerInput.text = "";
        GenerateNewQuestion();
    }

    void GenerateNewQuestion()
    {
        int number1 = Random.Range(1, 10 * currentQuestionDifficulty);
        int number2 = Random.Range(1, 10 * currentQuestionDifficulty);
        currentAnswer = number1 + number2;
        currentQuestion = "" + number1 + " " + number2 + "?";
        questionText.text = currentQuestion;
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
    void Update()
    {
        
    }
}
