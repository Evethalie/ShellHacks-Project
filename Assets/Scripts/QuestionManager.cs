using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public enum MathType
{
    Addition,
    Subtraction,
    Multiplication,
    Division,
}
public class QuestionManager : MonoBehaviour
{
    Player player;
    Enemy enemy;
    public GameManager gameManager;

    public Text questionText;
    public InputField answerInput;
    public Text feedback;
    public Text newAbilityText;

    [SerializeField] private int correctAnswers = 0;
    [SerializeField] private int incorrectAnswers = 0;
    [SerializeField] private int currentQuestionDifficulty = 1;
    public int abilitiesGained;

    [SerializeField] private string currentQuestion;
    [SerializeField] public int currentAnswer;

    public MathType[] mathChoice;
    [SerializeField] MathType currentChoice;

    public Canvas canvas;
    public Dropdown choices;

    int number1 = 0;
    int number2 = 0;
    public int newAbilities = 0;
    public int abilityIndexUp;

   public List <Object> abilityList;
    public int playerAnswer;

    public Canvas attackQuestionCanvas;

    private void Awake()
    {
        
       
        canvas.GetComponent<Canvas>().enabled = true;
        mathChoice = (MathType[])System.Enum.GetValues(typeof(MathType));
        
    }
    void Start()
    {
         player = FindObjectOfType<Player>();
       enemy = FindObjectOfType<Enemy>();
        GenerateNewQuestion(choices.value);
        
    }

    public void SubmitAnswer()
    {
        playerAnswer = int.Parse(answerInput.text);

        if (playerAnswer == currentAnswer)
        {
            feedback.text = "Correct!";
            correctAnswers++;
            incorrectAnswers = 0;
            AdjustDifficulty();
            GainAbility();
            player.DealDamage(enemy);
            
           
        }
        else
        {
            feedback.text = "Incorrect!";
            incorrectAnswers++;
            correctAnswers = 0;
            AdjustDifficulty();
            GainAbility();
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

    public void GainAbility()
    {
        if (correctAnswers >= 6)
        {
            newAbilityText.text = "You have gained an Ability! Answer and press " + abilitiesGained + " to use";
            abilitiesGained++;
           
            
        }
       
    }
    public void closeCanvas()
    {
        canvas.GetComponent<Canvas>().enabled = false;
    }

    public void PopulateList()
    {
       
    }

    public void UseAbility()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && abilitiesGained >= 1)
        {
            GenerateNewQuestion(choices.value);
            if (playerAnswer == currentAnswer)
            {
                Debug.Log("Used Ability 1");
                player.attackPower += 5;
                player.DealDamage(enemy);
                player.animator.SetTrigger("Attack");
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && abilitiesGained >= 2)
        {
           
            Debug.Log("Used Ability 2");
            abilityIndexUp = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && abilitiesGained >= 3)
        {
            
            Debug.Log("Used Ability 3");
            abilityIndexUp = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && abilitiesGained >= 4)
        {
            
            Debug.Log("Used Ability 4");
            abilityIndexUp = 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5) && abilitiesGained >= 5)
        {
            
            Debug.Log("Used Ability 5");
            abilityIndexUp = 4;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6) && abilitiesGained >= 6)
        {
            
            Debug.Log("Used Ability 6");
            abilityIndexUp = 5;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7)&& abilitiesGained >= 7)
        {
            
            Debug.Log("Used Ability 7");
            abilityIndexUp = 6;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8) && abilitiesGained >= 8)
        {
            
            Debug.Log("Used Ability 8");
            abilityIndexUp = 7;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9) && abilitiesGained >= 9)
        {
            
            Debug.Log("Used Ability 9");
            abilityIndexUp = 8;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0) && abilitiesGained >= 10)
        {
           
            Debug.Log("Used Ability 10");
            abilityIndexUp = 9;
        }
        

    }

    public void GenerateNewQuestionAbility()
    {

    }
        



    public void Update()
    {
        UseAbility();
        
    }

}
